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