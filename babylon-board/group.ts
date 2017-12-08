interface CardGroup {
    center: BABYLON.Vector3;
    add(num: number): void;
    remove(idx: number): void;
}

class HorizontalGroup implements CardGroup {
    private _scene: BABYLON.Scene;
    private _cards: Card[];

    center: BABYLON.Vector3;

    constructor(scene: BABYLON.Scene, center: BABYLON.Vector3) {
        this.center = center;
        this._scene = scene;
        this._cards = [];
    }

    add(num: number): void {
        console.log(`adding ${num} cards`)
        for (let i = 0; i < num; i++) {
            this._cards.push(new Card(this._scene, BABYLON.Color3.Yellow()))
        }
        this.refresh();
    }
    
    remove(idx: number): void {
        if (this._cards.length >= idx) {
            console.log(`removing card at index ${idx}`)
            // remove the object from the scene
            this._cards[idx].delete();
            // remove it from the card list also
            let merged = this._cards.slice(0, idx);
            if (this._cards.length > idx) {
                merged = merged.concat(this._cards.slice(idx + 1));
            }
            this._cards = merged;
            // realign
            this.refresh();
        }
    }

    private refresh() {
        const v = this.center;
        const n = this._cards.length;
        const gap = 0.5;
        const half = (n - 1) * (gap + Card.Width) / 2;
        const pos = new BABYLON.Vector3(v.x - half, v.y, v.z);
        console.log(`refresh ${n} cards`);
        for (let i = 0; i < n; i++) {
            this._cards[i].move(pos);
            pos.x += gap + Card.Width;
        }
    }
}

class CurvedGroup implements CardGroup {
    private _scene: BABYLON.Scene;
    private _cards: Card[];
    private _flip: boolean;
    private _height: number = 0.8;
    private _width: number = 2;

    center: BABYLON.Vector3;

    constructor(scene: BABYLON.Scene, center: BABYLON.Vector3, flip?: boolean) {
        this.center = center;
        this._scene = scene;
        this._cards = [];
        this._flip = flip;
    }

    add(num: number): void {
        console.log(`adding ${num} cards`)
        for (let i = 0; i < num; i++) {
            this._cards.push(new Card(this._scene, BABYLON.Color3.Yellow()))
        }
        this.refresh();
    }
    
    remove(idx: number): void {
        if (this._cards.length >= idx) {
            console.log(`removing card at index ${idx}`)
            // remove the object from the scene
            this._cards[idx].delete();
            // remove it from the card list also
            let merged = this._cards.slice(0, idx);
            if (this._cards.length > idx) {
                merged = merged.concat(this._cards.slice(idx + 1));
            }
            this._cards = merged;
            // realign
            this.refresh();
        }
    }

    private refresh() {
        let control = this.center.y
        control += this._flip ? -this._height : this._height;
        let curve = BABYLON.Curve3.CreateQuadraticBezier(
            new BABYLON.Vector3(this.center.x - this._width, this.center.y, this.center.z),
            new BABYLON.Vector3(this.center.x, control, this.center.z),
            new BABYLON.Vector3(this.center.x + this._width, this.center.y, this.center.z),
            this._cards.length - 1);
        let points = curve.getPoints();
        let path3d = new BABYLON.Path3D(points);
        let normals = path3d.getNormals();
        let tangents = path3d.getTangents();
        let binormals = path3d.getBinormals();
        let li = BABYLON.Mesh.CreateLines('li', path3d.getCurve(), this._scene);

        console.log(`refresh ${this._cards.length} cards`);
        for (let i = 0; i < this._cards.length; i++) {
            let dot = BABYLON.Vector3.Dot(
                BABYLON.Vector3.Normalize(points[i]), 
                BABYLON.Vector3.Normalize(binormals[i])
            );
            let angle = Math.acos(dot);
            this._cards[i].move(points[i]);
            
            // show the orthognal axes of the points to the curve
            var tg = BABYLON.Mesh.CreateLines('tg', [ points[i], points[i].add(tangents[i]) ], this._scene);
            tg.color = BABYLON.Color3.Red();
            var no = BABYLON.Mesh.CreateLines('no', [ points[i], points[i].add(normals[i]) ], this._scene);
            no.color = BABYLON.Color3.Blue();
            var bi = BABYLON.Mesh.CreateLines('bi', [ points[i], points[i].add(binormals[i]) ], this._scene);
            bi.color = BABYLON.Color3.Green();
            // align to card to curve
            let rotation = BABYLON.Vector3.RotationFromAxis(tangents[i], binormals[i], normals[i]);
            this._cards[i].rotation(rotation);
        }
    }
}