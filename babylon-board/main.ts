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

class Card {
    private _mesh: BABYLON.Mesh;

    public static Width: number = 0.7;

    constructor(scene: BABYLON.Scene, color: BABYLON.Color3) {
        this._mesh = BABYLON.MeshBuilder.CreatePlane("card1", { 
            width: Card.Width, 
            height: Card.Width * 1.4, 
            sideOrientation: BABYLON.Mesh.DOUBLESIDE 
        }, scene);
        let mat = new BABYLON.StandardMaterial("cardMat", scene);
        mat.emissiveColor = color;
        this._mesh.material = mat;        
    }

    move(v: BABYLON.Vector3): void {
        this._mesh.position = new BABYLON.Vector3(v.x, v.y, v.z);
    }

    delete(): void {
        this._mesh.dispose();
    }
}

class Board {
    private _mesh: BABYLON.Mesh;
    private _scene: BABYLON.Scene;

    constructor(scene: BABYLON.Scene, color: BABYLON.Color3) {
        this._mesh = BABYLON.MeshBuilder.CreatePlane("board", 
            { width: 10, height: 7 }, scene);
        let mat = new BABYLON.StandardMaterial("boardMat", scene);
        mat.emissiveColor = color;
        this._mesh.material = mat;        
    }

    addCards(n: number, v: BABYLON.Vector3): void {
        let gap = 0.5;
        let half = (n - 1) * (gap + Card.Width) / 2;
        let pos = new BABYLON.Vector3(v.x - half, v.y, v.z);
        for (let i = 0; i < n; i++) {
            let card = new Card(this._scene, BABYLON.Color3.Yellow());
            card.move(pos);
            pos.x += gap + Card.Width;
        }
    }
}

class Game {
    private _canvas: HTMLCanvasElement;
    private _engine: BABYLON.Engine;
    private _scene: BABYLON.Scene;
    private _camera: BABYLON.FreeCamera;
    private _light: BABYLON.Light;

    constructor(canvasElement : string) {
        this._canvas = document.getElementById(canvasElement) as HTMLCanvasElement;
        this._engine = new BABYLON.Engine(this._canvas, true);
    }
  
    createScene() : void {
        this._scene = new BABYLON.Scene(this._engine);
        this._camera = new BABYLON.FreeCamera('camera1', new BABYLON.Vector3(0, -2, -10), this._scene);
        this._camera.setTarget(BABYLON.Vector3.Zero());
        this._camera.attachControl(this._canvas, true);
        
        this._light = new BABYLON.HemisphericLight('light1', new BABYLON.Vector3(0,1,0), this._scene);
        this._light.intensity = 0.7;

        let board = new Board(this._scene, BABYLON.Color3.Teal())

        let friendly = new HorizontalGroup(this._scene, new BABYLON.Vector3(0, -1, -0.2));
        let enemy = new HorizontalGroup(this._scene, new BABYLON.Vector3(0, 1, -0.2));
               
        setTimeout(() => {
            friendly.add(7);
            setTimeout(() => {
                friendly.remove(3);
            }, 3000);
        }, 3000);
        
        enemy.add(4);
        
        // board.addCards(7, );
        // board.addCards(4, new BABYLON.Vector3(0, 1, -0.2));

        // board.addCards(2, new BABYLON.Vector3(0, -3.2, -0.2));
        // board.addCards(5, new BABYLON.Vector3(0, 3.2, -0.2));
    }
  
    doRender() : void {
        // run the render loop
        this._engine.runRenderLoop(() => {
            this._scene.render();
        });
        // the canvas/window resize event handler
        window.addEventListener('resize', () => {
            this._engine.resize();
        });
    }
}
  
window.addEventListener('DOMContentLoaded', () => {
    let game = new Game('renderCanvas');
    game.createScene();
    game.doRender();
});