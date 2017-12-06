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

    rotate(t: number): void {
        this._mesh.rotation = new BABYLON.Vector3(0, 0, t);
    }

    delete(): void {
        this._mesh.dispose();
    }
}

class Board {
    private _mesh: BABYLON.Mesh;
    private _scene: BABYLON.Scene;

    friendly: CardGroup;
    enemy: CardGroup;
    playerHand: CardGroup;
    opponentHand: CardGroup;
    
    constructor(scene: BABYLON.Scene, color: BABYLON.Color3) {
        this._mesh = BABYLON.MeshBuilder.CreatePlane("board", 
            { width: 10, height: 7 }, scene);
        let mat = new BABYLON.StandardMaterial("boardMat", scene);
        mat.emissiveColor = color;
        this._mesh.material = mat;
        // init zones
        this.friendly = new HorizontalGroup(this._scene, new BABYLON.Vector3(0, -1, -0.2));
        this.enemy = new HorizontalGroup(this._scene, new BABYLON.Vector3(0, 1, -0.2));
        this.playerHand = new ArcGroup(this._scene, new BABYLON.Vector3(0, -3.2, -0.2));
        this.opponentHand = new HorizontalGroup(this._scene, new BABYLON.Vector3(0, 3.2, -0.2));
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

        let disc = BABYLON.MeshBuilder.CreateDisc("disc", {radius: 1, tessellation: 20}, this._scene);
        disc.position = new BABYLON.Vector3(0, -3.2, -0.1)
        let mat = new BABYLON.StandardMaterial("discmat", this._scene);
        mat.emissiveColor = BABYLON.Color3.Green();
        disc.material = mat;

        let friendly = new HorizontalGroup(this._scene, new BABYLON.Vector3(0, -1, -0.2));
        let enemy = new HorizontalGroup(this._scene, new BABYLON.Vector3(0, 1, -0.2));
               
        board.friendly.add(7);
        board.enemy.add(4);
        board.playerHand.add(6);
        board.opponentHand.add(5);

        setTimeout(() => {
            board.friendly.remove(3);
        }, 3000);

        setTimeout(() => {
            board.opponentHand.remove(4);
            board.enemy.add(1);
        }, 5000);
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