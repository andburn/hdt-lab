(function() {

  var simple = function() {
    var scene = new THREE.Scene();
    var camera = new THREE.PerspectiveCamera( 70, window.innerWidth / window.innerHeight, 1, 1000 );
    camera.position.y = 2.6;
    camera.lookAt(scene.position);
    camera.rotateOnAxis(new THREE.Vector3( 0, 0, 1 ), -1.57);

    var renderer = new THREE.WebGLRenderer( { antialias: true, alpha: true } );
    renderer.setSize( window.innerWidth, window.innerHeight );
    renderer.setClearColor( 0xffffff, 0);
    document.body.appendChild( renderer.domElement );

    components.base(scene);
    components.portrait(scene);
    components.namebanner(scene);
    components.description(scene);
    components.dragon(scene);
    components.raceplate(scene);
    components.rarityFrame(scene);

    function render() {
    	requestAnimationFrame( render );
    	renderer.render( scene, camera );
    }
    render();
  }

  var setup = function() {
    var container, stats;
		var camera, scene, card, renderer;
		var targetRotation = 0;
		var targetRotationOnMouseDown = 0;
		var mouseX = 0;
		var mouseXOnMouseDown = 0;
		var windowHalfX = window.innerWidth / 2;
		var windowHalfY = window.innerHeight / 2;

    function onWindowResize() {

      windowHalfX = window.innerWidth / 2;
      windowHalfY = window.innerHeight / 2;

      camera.aspect = window.innerWidth / window.innerHeight;
      camera.updateProjectionMatrix();

      renderer.setSize( window.innerWidth, window.innerHeight );

    }

    function onDocumentMouseDown( event ) {

      event.preventDefault();

      document.addEventListener( 'mousemove', onDocumentMouseMove, false );
      document.addEventListener( 'mouseup', onDocumentMouseUp, false );
      document.addEventListener( 'mouseout', onDocumentMouseOut, false );

      mouseXOnMouseDown = event.clientX - windowHalfX;
      targetRotationOnMouseDown = targetRotation;

    }

    function onDocumentMouseMove( event ) {

      mouseX = event.clientX - windowHalfX;

      targetRotation = targetRotationOnMouseDown + ( mouseX - mouseXOnMouseDown ) * 0.02;

    }

    function onDocumentMouseUp( event ) {

      document.removeEventListener( 'mousemove', onDocumentMouseMove, false );
      document.removeEventListener( 'mouseup', onDocumentMouseUp, false );
      document.removeEventListener( 'mouseout', onDocumentMouseOut, false );

    }

    function onDocumentMouseOut( event ) {

      document.removeEventListener( 'mousemove', onDocumentMouseMove, false );
      document.removeEventListener( 'mouseup', onDocumentMouseUp, false );
      document.removeEventListener( 'mouseout', onDocumentMouseOut, false );

    }

    function onDocumentTouchStart( event ) {

      if ( event.touches.length === 1 ) {

        event.preventDefault();

        mouseXOnMouseDown = event.touches[ 0 ].pageX - windowHalfX;
        targetRotationOnMouseDown = targetRotation;

      }

    }

    function onDocumentTouchMove( event ) {

      if ( event.touches.length === 1 ) {

        event.preventDefault();

        mouseX = event.touches[ 0 ].pageX - windowHalfX;
        targetRotation = targetRotationOnMouseDown + ( mouseX - mouseXOnMouseDown ) * 0.05;

      }

    }

    /* Setup scene and renderer */

		container = document.createElement( 'div' );
		document.body.appendChild( container );

		// var info = document.createElement( 'div' );
		// info.style.position = 'absolute';
		// info.style.top = '10px';
		// info.style.width = '100%';
		// info.style.textAlign = 'center';
		// info.innerHTML = 'Drag to spin the cube';
		// container.appendChild( info );

		// camera = new THREE.PerspectiveCamera( 70, window.innerWidth / window.innerHeight, 1, 1000 );
		// camera.position.y = 150;
		// camera.position.z = 500;

    scene = new THREE.Scene();

    camera = new THREE.PerspectiveCamera( 70, window.innerWidth / window.innerHeight, 1, 1000 );
    camera.position.y = 3;
    camera.lookAt(scene.position);
    camera.rotateOnAxis(new THREE.Vector3( 0, 0, 1 ), -1.57);

    renderer = new THREE.WebGLRenderer( { antialias: true, alpha: true } );
    renderer.setClearColor( 0xf0f0f0 );
    renderer.setPixelRatio( window.devicePixelRatio );
    renderer.setSize( window.innerWidth, window.innerHeight );
    container.appendChild( renderer.domElement );

    card = new THREE.Object3D();
    components.load(card);
    scene.add(card);
    console.log(card);

		// stats = new Stats();
		// stats.domElement.style.position = 'absolute';
		// stats.domElement.style.top = '0px';
		// container.appendChild( stats.domElement );

		document.addEventListener( 'mousedown', onDocumentMouseDown, false );
		document.addEventListener( 'touchstart', onDocumentTouchStart, false );
		document.addEventListener( 'touchmove', onDocumentTouchMove, false );

		window.addEventListener( 'resize', onWindowResize, false );

    function render() {
      requestAnimationFrame( render );
      renderer.render( scene, camera );
      card.rotation.z += ( targetRotation - card.rotation.z ) * 0.05;
      //stats.update();
    }
    render();
  }

  setup();

})();
