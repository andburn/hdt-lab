(function() {
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

})();
