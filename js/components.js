var components = (function(){

  var addMesh = function(scene, texName, meshName, x, y, z, scale, offX, offY) {
    var texture = new THREE.Texture();
    var loader = new THREE.ImageLoader();
    loader.load( 'textures/' + texName, function ( image ) {
      texture.image = image;
      if (offX != null) {
        texture.offset.x = offX;
        texture.offset.y = offY;
      }
      texture.needsUpdate = true;
    } );

    loader = new THREE.OBJLoader();
    loader.load( 'meshes/' + meshName, function ( object ) {
      object.traverse( function ( child ) {
        if ( child instanceof THREE.Mesh ) {
          child.material = new THREE.MeshBasicMaterial( { map: texture } );
        }
      } );
      object.scale.set(scale, scale, scale);
      object.position.set(x, y, z);
      scene.add( object );
    } );
  }

  var base = function(scene) {
    addMesh( scene,
      'Card_Inhand_Generic.png',
      'Card_InHand_Ally_Base_mesh.obj',
      0, 0, 0, 1, null, null
    );
  };

  var portrait = function(scene) {
    addMesh( scene,
      'Card_Inhand_Generic.png',
      'Card_InHand_Ally_Portrait_mesh.obj',
      -0.0115, 0.0677, -0.6590, 1, null, null
    );
  };

  var namebanner = function(scene) {
    addMesh( scene,
      'Card_InHand_BannerAtlas.png',
      'AllyInHand_NameBanner_mesh.obj',
      -0.0016, 0.143, 0.0877, 1, null, null
    );
  }

  var description = function(scene) {
    addMesh( scene,
      'Card_InHand_BannerAtlas.png',
      'Card_InHand_Ally_Description_mesh.obj',
      -0.02314, 0.0528, 0.7809, 1, null, null
    );
  }

  var dragon = function(scene) {
    addMesh( scene,
      'Card_InHand_BannerAtlas.png',
      'Card_InHand_Ally_EliteDragon_mesh.obj',
      -0.01148, 0.0676, -0.6589, 1, null, null
    );
  }

  var raceplate = function(scene) {
    addMesh( scene,
      'Card_InHand_BannerAtlas.png',
      'AllyInHand_RacePlate_mesh.obj',
      -0.07, 0.056, 1.19, 1, null, null
    );
  }

  var rarityFrame = function(scene) {
    addMesh(scene,
      'Card_Inhand_Generic.png',
      'Card_InHand_Ally_RarityGemFrame_mesh.obj',
      -0.0115, 0.0677, -0.6590, 1, -0.436, 0.132
    );
  }

  return {
    base: base,
    portrait: portrait,
    namebanner: namebanner,
    description: description,
    rarityFrame: rarityFrame,
    dragon: dragon,
    raceplate: raceplate
  };

})();
