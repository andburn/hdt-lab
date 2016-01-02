(function() {
  var scene = new THREE.Scene();
  var camera = new THREE.PerspectiveCamera( 75, window.innerWidth / window.innerHeight, 1, 1000 );
  camera.position.z = 4;

  var renderer = new THREE.WebGLRenderer();
  renderer.setSize( window.innerWidth, window.innerHeight );
  document.body.appendChild( renderer.domElement );

  var mapSrc = "images/minion.png";
  var map = THREE.ImageUtils.loadTexture(mapSrc);

  var geometry = new THREE.PlaneGeometry( 3, 3 );
  var material = new THREE.MeshBasicMaterial( { map: map } );
  var plane = new THREE.Mesh( geometry, material );
  scene.add( plane );

  function render() {
  	requestAnimationFrame( render );
  	renderer.render( scene, camera );
  }
  render();

})();
