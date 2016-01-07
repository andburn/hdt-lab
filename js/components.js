var components = (function(){

  var loadMesh = function(obj, textures, meshName, x, y, z, scale) {
    var loader, mat, materials = [];

    mat = new THREE.MultiMaterial(materials);
    loader = new THREE.ImageLoader();
    for (var i = 0; i < textures.length; i++) {
      loader.load( 'textures/' + textures[i], function ( image ) {
        var texture = new THREE.Texture();
        texture.image = image;
        texture.needsUpdate = true;
        materials.push(new THREE.MeshBasicMaterial( { map: texture }));
        //mat.needsUpdate = true;
      } );
    }

    loader = new THREE.OBJLoader2();
    loader.load( 'meshes/' + meshName, function ( object ) {
      object.traverse( function ( child ) {
        if ( child instanceof THREE.Mesh ) {
          child.material = mat;
          child.material.needsUpdate = true;
        }
      } );
      object.scale.set(scale, scale, scale);
      object.position.set(x, y, z);
      obj.add(object);
    } );
  }

  var base = function(obj) {
    loadMesh(obj,
      ['Card_Back_Default_D.png', 'Card_Inhand_Generic.png'],
      'Card_InHand_Ally_Base_mesh.obj',
      0, 0, 0, 1, null, null
    );
  };

  var portrait = function(obj) {
    loadMesh(obj,
      ['GVG_119.png', 'Card_Inhand_Generic.png'],
      'Card_InHand_Ally_Portrait_mesh.obj',
      -0.0115, 0.0677, -0.6590, 1, null, null
    );
  };

  var namebanner = function(obj) {
    loadMesh(obj,
      ['Card_InHand_BannerAtlas2.png'],
      'AllyInHand_NameBanner_mesh.obj',
      -0.0016, 0.143, 0.0877, 1, null, null
    );
  }

  var description = function(obj) {
    loadMesh(obj,
      ['Card_InHand_BannerAtlas2.png'],
      'Card_InHand_Ally_Description_mesh.obj',
      -0.02314, 0.0528, 0.7809, 1, null, null
    );
  }

  var dragon = function(obj) {
    loadMesh(obj,
      ['Card_InHand_BannerAtlas2.png'],
      'Card_InHand_Ally_EliteDragon_mesh.obj',
      -0.01148, 0.0676, -0.6589, 1, null, null
    );
  }

  var raceplate = function(obj) {
    loadMesh(obj,
      ['Card_InHand_BannerAtlas2.png'],
      'AllyInHand_RacePlate_mesh.obj',
      -0.07, 0.056, 1.19, 1, null, null
    );
  }

  var rarityFrame = function() {
    createMesh(
      ['Card_Inhand_Generic.png'],
      'Card_InHand_Ally_RarityGemFrame_mesh.obj',
      -0.0115, 0.0677, -0.6590, 1, -0.436, 0.132
    );
  }

  var load = function(obj) {
    portrait(obj);
    base(obj);
    namebanner(obj);
    description(obj);
    dragon(obj);
    raceplate(obj);
    // obj.add(rarityFrame());
  }

  return {
    load: load
  };

})();
