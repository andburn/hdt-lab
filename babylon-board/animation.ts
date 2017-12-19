class Game {
    private _canvas: HTMLCanvasElement;
    private _engine: BABYLON.Engine;
    private _scene: BABYLON.Scene;
    private _camera: BABYLON.FreeCamera;
    private _light: BABYLON.Light;
    private _material: BABYLON.StandardMaterial;

    constructor(canvasElement : string) {
        this._canvas = document.getElementById(canvasElement) as HTMLCanvasElement;
        this._engine = new BABYLON.Engine(this._canvas, true);
        this.animate = this.animate.bind(this);
        this.animatePlay = this.animatePlay.bind(this);
    }
  
    createCard(pos?: BABYLON.Vector3): BABYLON.Mesh {
        const width = 0.8;
        let mesh = BABYLON.MeshBuilder.CreatePlane("card", { 
            width: width,
            height: width * 1.4,
            sideOrientation: BABYLON.Mesh.DOUBLESIDE
        }, this._scene);
        mesh.material = this._material;
        if (pos) {
            mesh.position = pos;
        }
        return mesh;
    }

    animate(mesh: BABYLON.Mesh, target: BABYLON.Vector3, callback?: any): void {
        const frameRate = 30;
        const pickPosition = new BABYLON.Vector3(mesh.position.x - 1, mesh.position.y, mesh.position.z - 0.8);

        // draw out and up
        const aniPick = new BABYLON.Animation("aniPick", "position", frameRate, 
            BABYLON.Animation.ANIMATIONTYPE_VECTOR3, BABYLON.Animation.ANIMATIONLOOPMODE_CYCLE);        
        const aniPickKeys = [
            { 
                frame: 0, 
                value: mesh.position 
            }, { 
                frame: 1 * frameRate, 
                value: pickPosition
            }
        ];
        aniPick.setKeys(aniPickKeys);

        // flip face up
        const aniFlip = new BABYLON.Animation("aniFlip", "rotation.y", frameRate, 
            BABYLON.Animation.ANIMATIONTYPE_FLOAT, BABYLON.Animation.ANIMATIONLOOPMODE_CYCLE);        
        const aniFlipKeys = [
            { 
                frame: 0.5 * frameRate, 
                value: mesh.rotation.y 
            }, { 
                frame: 1 * frameRate, 
                value: mesh.rotation.y + 3.14
            }
        ];
        aniFlip.setKeys(aniFlipKeys);

        // move to target position
        const aniMove = new BABYLON.Animation("aniMove", "position", frameRate, 
        BABYLON.Animation.ANIMATIONTYPE_VECTOR3, BABYLON.Animation.ANIMATIONLOOPMODE_CYCLE);        
        const aniMoveKeys = [
            { 
                frame: 0, 
                value: pickPosition 
            }, { 
                frame: 2 * frameRate, 
                value: target
            }
        ];
        aniMove.setKeys(aniMoveKeys);
        // add easing function for animation end (info: http://easings.net/)
        const easeMove = new BABYLON.CubicEase();
        easeMove.setEasingMode(BABYLON.EasingFunction.EASINGMODE_EASEOUT);
        aniMove.setEasingFunction(easeMove);

        // do animation, after flip do move
        this._scene.beginDirectAnimation(mesh, [aniPick, aniFlip], 0, 1 * frameRate, false, 1, function() {
            this._scene.beginDirectAnimation(mesh, [aniMove], 0, 2 * frameRate, false, 1, function() {
                callback(mesh, new BABYLON.Vector3(0, -0.5, -0.2));
            });
        });
    }

    animatePlay(mesh: BABYLON.Mesh, target: BABYLON.Vector3): void {
        const frameRate = 30;
        const hoverPosition = new BABYLON.Vector3(
            mesh.position.x, mesh.position.y, mesh.position.z - 2.5);

        // lift card up
        const aniHover = new BABYLON.Animation("aniHover", "position", frameRate, 
            BABYLON.Animation.ANIMATIONTYPE_VECTOR3, BABYLON.Animation.ANIMATIONLOOPMODE_CYCLE);        
        const aniHoverKeys = [
            { 
                frame: 0, 
                value: mesh.position 
            }, { 
                frame: 1 * frameRate, 
                value: hoverPosition
            }
        ];
        aniHover.setKeys(aniHoverKeys);
        // add some sway on lift
        const aniSway = new BABYLON.Animation("aniSway", "rotation.z", frameRate, 
            BABYLON.Animation.ANIMATIONTYPE_FLOAT, BABYLON.Animation.ANIMATIONLOOPMODE_CYCLE);        
        const aniSwayKeys = [
            { 
                frame: 0, 
                value: 0 
            }, { 
                frame: 0.25 * frameRate, 
                value: 0.1
            }, { 
                frame: 0.5 * frameRate, 
                value: -0.1
            }, { 
                frame: 0.75 * frameRate, 
                value: 0.1
            }, { 
                frame: 1 * frameRate, 
                value: 0
            }
        ];
        aniSway.setKeys(aniSwayKeys);
        // add some sway on lift
        const aniMove = new BABYLON.Animation("aniMove", "position", frameRate, 
            BABYLON.Animation.ANIMATIONTYPE_VECTOR3, BABYLON.Animation.ANIMATIONLOOPMODE_CYCLE);        
        const aniMoveKeys = [
            { 
                frame: 0, 
                value: hoverPosition
            }, { 
                frame: 2 * frameRate, 
                value: target
            }
        ];
        aniMove.setKeys(aniMoveKeys);
        // add easing function
        const ease1 = new BABYLON.QuadraticEase();
        ease1.setEasingMode(BABYLON.EasingFunction.EASINGMODE_EASEINOUT);
        aniMove.setEasingFunction(ease1);        
        
        this._scene.beginDirectAnimation(mesh, [aniHover, aniSway], 0, 2 * frameRate, false, 1, function() {
            this._scene.beginDirectAnimation(mesh, [aniMove], 0, 2 * frameRate, false);
        });
    }

    createScene() : void {
        this._scene = new BABYLON.Scene(this._engine);
        this._camera = new BABYLON.FreeCamera('camera1', new BABYLON.Vector3(0, -2, -10), this._scene);
        this._camera.setTarget(BABYLON.Vector3.Zero());
        this._camera.attachControl(this._canvas, true);

        this._light = new BABYLON.HemisphericLight('ambient', new BABYLON.Vector3(0,1,0), this._scene);
        this._light.intensity = 0.4;
        var light = new BABYLON.DirectionalLight("DirectionalLight", new BABYLON.Vector3(0, 0, 1), this._scene);
        light.position = new BABYLON.Vector3(0, 0, -10);
        light.intensity = 0.5;

        // set the card material
        this._material = new BABYLON.StandardMaterial("cardMat", this._scene);
        this._material.diffuseTexture = new BABYLON.Texture("./babylon_card.png", this._scene);

        // add the base board
        let board = BABYLON.MeshBuilder.CreatePlane("board", 
            { width: 10, height: 7 }, this._scene);
        let boardMaterial = new BABYLON.StandardMaterial("boardMat", this._scene);
        boardMaterial.diffuseColor = BABYLON.Color3.FromInts(160, 140, 0);
        board.material = boardMaterial;        

        // set positions
        const startA = new BABYLON.Vector3(4.2, 0, -0.2);
        const finishA = new BABYLON.Vector3(0, -2.5, -0.2);

        // add cards to animate
        let placeholder1 = this.createCard(startA);
        let card1 = this.createCard(startA);
        
        // generate shadows
        const shadowGen = new BABYLON.ShadowGenerator(1024, light);
        shadowGen.getShadowMap().renderList.push(card1);
        placeholder1.receiveShadows = true;
        board.receiveShadows = true;

        this.animate(card1, finishA, this.animatePlay);
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