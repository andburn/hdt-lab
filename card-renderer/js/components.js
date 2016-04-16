var components = (function(){

  var loadShader = function() {
    var v = document.getElementById("vertexshader").textContent;
    var f = document.getElementById("fragmentshader").textContent;
    return {
      vertex: v,
      fragment: f
    };
  };

  var addMesh = function(obj, texture1, texture2, meshName, x, y, z, shader) {

    var loader = loader = new THREE.ImageLoader();
    loader.load( 'textures/' + texture1, function ( image ) {
      var mainTex = new THREE.Texture();
      mainTex.image = image;
      mainTex.needsUpdate = true;
      var shaderMaterial = new THREE.ShaderMaterial({
        uniforms: {
          mainTexture: {type: 't', value: mainTex} ,
          mainTextureST: { type: "v4", value: new THREE.Vector4( 1, 1, 0, 0 ) }
        },
        vertexColors: THREE.VertexColors,
        vertexShader:   shader.vertex,
        fragmentShader: shader.fragment
      });

      var objloader = new THREE.OBJLoader();
      objloader.load( 'meshes/' + meshName, function ( object ) {
        object.traverse( function ( child ) {
          if ( child instanceof THREE.Mesh ) {
            child.material = shaderMaterial;
            child.material.needsUpdate = true;
          }
        } );
        object.position.set(x, y, z);
        obj.add(object);
      } );
    });
  };

  var loadMesh = function(obj, textures, meshName, x, y, z, scale, shader) {
    var loader, mat, materials = [];

    mat = new THREE.MultiMaterial(materials);
    loader = new THREE.ImageLoader();
    for (var i = 0; i < textures.length; i++) {
      loader.load( 'textures/' + textures[i], function ( image ) {
        var texture = new THREE.Texture();
        texture.image = image;
        texture.needsUpdate = true;
        shader = loadShader();
        if (shader === undefined || shader === null) {
          materials.push(new THREE.MeshBasicMaterial( { map: texture }));
        } else {
          console.log("adding shader material");
          console.log(shader);
          var shaderMaterial = new THREE.ShaderMaterial({
        		uniforms: {
              mainTexture: {type: 't', value: texture} ,
              mainTextureST: { type: "v4", value: new THREE.Vector4( 1, 1, 0, 0 )
            }},
            vertexColors: THREE.VertexColors,
        		vertexShader:   shader.vertex,
        		fragmentShader: shader.fragment
        	});
          materials.push(shaderMaterial);
        }
        //mat.needsUpdate = true;
      } );
    }

    loader = new THREE.OBJLoader();
    loader.load( 'meshes/' + meshName, function ( object ) {
      object.traverse( function ( child ) {
        if ( child instanceof THREE.Mesh ) {
          child.material = materials[0];
          child.material.needsUpdate = true;
        }
      } );
      object.scale.set(scale, scale, scale);
      object.position.set(x, y, z);
      obj.add(object);
    } );
  }

  var base = function(obj, shader) {
    addMesh(obj,
      'Card_Inhand_Generic.png', null,
      'Card_InHand_Ally_Base_mesh.obj',
      0, 0, 0, shader
    );
  };

  var portrait = function(obj, shader) {
    addMesh(obj,
      'GVG_119.png', 'Card_Inhand_Generic.png',
      'Card_InHand_Ally_Portrait_mesh.obj',
      -0.0115, 0.0677, -0.6590, shader
    );
  };

  var namebanner = function(obj, shader) {
    addMesh(obj,
      'Card_InHand_BannerAtlas2.png', null,
      'AllyInHand_NameBanner_mesh.obj',
      -0.0016, 0.143, 0.0877, shader
    );
  }

  var description = function(obj, shader) {
    addMesh(obj,
      'Card_InHand_BannerAtlas2.png', null,
      'Card_InHand_Ally_Description_mesh.obj',
      -0.02314, 0.0528, 0.7809, shader
    );
  }

  var dragon = function(obj, shader) {
    addMesh(obj,
      'Card_InHand_BannerAtlas2.png', null,
      'Card_InHand_Ally_EliteDragon_mesh.obj',
      -0.01148, 0.0676, -0.6589, shader
    );
  }

  var raceplate = function(obj, shader) {
    addMesh(obj,
      'Card_InHand_BannerAtlas2.png', null,
      'AllyInHand_RacePlate_mesh.obj',
      -0.07, 0.056, 1.19, shader
    );
  }

  var rarityFrame = function() {
    loadMesh(
      ['Card_Inhand_Generic.png'],
      'Card_InHand_Ally_RarityGemFrame_mesh.obj',
      -0.0115, 0.0677, -0.6590, 1, -0.436, 0.132
    );
  }

  var load = function(obj) {
    var card_unlit = loadShader();
    portrait(obj, card_unlit);
    base(obj, card_unlit);
    namebanner(obj, card_unlit);
    description(obj, card_unlit);
    dragon(obj, card_unlit);
    raceplate(obj, card_unlit);
    // obj.add(rarityFrame());
  }

  return {
    load: load
  };

})();
