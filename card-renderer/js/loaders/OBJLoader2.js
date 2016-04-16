THREE.OBJLoader2 = function ( manager ) {

	this.manager = ( manager !== undefined ) ? manager : THREE.DefaultLoadingManager;

};

THREE.OBJLoader2.prototype = {

	constructor: THREE.OBJLoader2,

  load: function ( url, onLoad, onProgress, onError ) {

		var scope = this;

		var loader = new THREE.XHRLoader( scope.manager );
		loader.setCrossOrigin( this.crossOrigin );
		loader.load( url, function ( text ) {

			onLoad( scope.parse( text ) );

		}, onProgress, onError );

	},

	setCrossOrigin: function ( value ) {

		this.crossOrigin = value;

	},

  parse: function ( text ) {

		/* Declare variables */

		var object = new THREE.Object3D();

		var geometry = new THREE.Geometry();
		var material = new THREE.MeshBasicMaterial();
		var mesh = new THREE.Mesh( geometry, material );

		var vertices = [];
		var normals = [];
		var uvs = [];
		var groups = [];

		var vertex_pattern = /v( +[\d|\.|\+|\-|e|E]+)( +[\d|\.|\+|\-|e|E]+)( +[\d|\.|\+|\-|e|E]+)/;
		var normal_pattern = /vn( +[\d|\.|\+|\-|e|E]+)( +[\d|\.|\+|\-|e|E]+)( +[\d|\.|\+|\-|e|E]+)/;
		var uv_pattern = /vt( +[\d|\.|\+|\-|e|E]+)( +[\d|\.|\+|\-|e|E]+)/;
		var face_pattern1 = /f( +-?\d+)( +-?\d+)( +-?\d+)( +-?\d+)?/;
		var face_pattern2 = /f( +(-?\d+)\/(-?\d+))( +(-?\d+)\/(-?\d+))( +(-?\d+)\/(-?\d+))( +(-?\d+)\/(-?\d+))?/;
		var face_pattern3 = /f( +(-?\d+)\/(-?\d+)\/(-?\d+))( +(-?\d+)\/(-?\d+)\/(-?\d+))( +(-?\d+)\/(-?\d+)\/(-?\d+))( +(-?\d+)\/(-?\d+)\/(-?\d+))?/;
		var face_pattern4 = /f( +(-?\d+)\/\/(-?\d+))( +(-?\d+)\/\/(-?\d+))( +(-?\d+)\/\/(-?\d+))( +(-?\d+)\/\/(-?\d+))?/;

		/* Declare helper functions */

		function vector( x, y, z ) {
			return new THREE.Vector3( x, y, z );
		}

		function uv( u, v ) {
			return new THREE.Vector2( u, v );
		}

		function face3( a, b, c, normals ) {
			var f = new THREE.Face3( a, b, c, normals );
			if (mtlCount > 0) {
				f.materialIndex = mtlCount - 1;
			}
			return f;
		}

		function add_face( a, b, c, normals_inds ) {

			if ( normals_inds === undefined ) {

				geometry.faces.push( face3(
					parseInt( a ) - ( face_offset + 1 ),
					parseInt( b ) - ( face_offset + 1 ),
					parseInt( c ) - ( face_offset + 1 )
				) );

			} else {

				geometry.faces.push( face3(
					parseInt( a ) - ( face_offset + 1 ),
					parseInt( b ) - ( face_offset + 1 ),
					parseInt( c ) - ( face_offset + 1 ),
					[
						normals[ parseInt( normals_inds[ 0 ] ) - 1 ].clone(),
						normals[ parseInt( normals_inds[ 1 ] ) - 1 ].clone(),
						normals[ parseInt( normals_inds[ 2 ] ) - 1 ].clone()
					]
				) );

			}

		}

		function add_uvs( a, b, c ) {
			geometry.faceVertexUvs[ 0 ].push( [
				uvs[ parseInt( a ) - 1 ].clone(),
				uvs[ parseInt( b ) - 1 ].clone(),
				uvs[ parseInt( c ) - 1 ].clone()
			] );
		}

		function handle_face_line( faces, uvs, normals_inds ) {

			if ( faces[ 3 ] === undefined ) {

				add_face( faces[ 0 ], faces[ 1 ], faces[ 2 ], normals_inds );

				if ( ! ( uvs === undefined ) && uvs.length > 0 ) {

					add_uvs( uvs[ 0 ], uvs[ 1 ], uvs[ 2 ] );

				}

			} else {

				if ( ! ( normals_inds === undefined ) && normals_inds.length > 0 ) {

					add_face( faces[ 0 ], faces[ 1 ], faces[ 3 ], [ normals_inds[ 0 ], normals_inds[ 1 ], normals_inds[ 3 ] ] );
					add_face( faces[ 1 ], faces[ 2 ], faces[ 3 ], [ normals_inds[ 1 ], normals_inds[ 2 ], normals_inds[ 3 ] ] );

				} else {

					add_face( faces[ 0 ], faces[ 1 ], faces[ 3 ] );
					add_face( faces[ 1 ], faces[ 2 ], faces[ 3 ] );

				}

				if ( ! ( uvs === undefined ) && uvs.length > 0 ) {

					add_uvs( uvs[ 0 ], uvs[ 1 ], uvs[ 3 ] );
					add_uvs( uvs[ 1 ], uvs[ 2 ], uvs[ 3 ] );

				}

			}

		}

		/* Main parse logic */

		var mtlCount = 0;
		var face_offset = 0;
		var lines = text.split( "\n" );
		//console.log("lines: " + lines.length);

		for ( var i = 0; i < lines.length; i ++ ) {

			var line = lines[ i ];
			line = line.trim();

			if ( line.length === 0 || line.charAt( 0 ) === '#' ) {
				continue;
			} else if ( ( result = vertex_pattern.exec( line ) ) !== null ) {
				// ["v 1.0 2.0 3.0", "1.0", "2.0", "3.0"]
				vertices.push( vector(
					parseFloat( result[ 1 ] ),
					parseFloat( result[ 2 ] ),
					parseFloat( result[ 3 ] )
				) );
			} else if ( ( result = normal_pattern.exec( line ) ) !== null ) {
				// ["vn 1.0 2.0 3.0", "1.0", "2.0", "3.0"]
				normals.push( vector(
					parseFloat( result[ 1 ] ),
					parseFloat( result[ 2 ] ),
					parseFloat( result[ 3 ] )
				) );
			} else if ( ( result = uv_pattern.exec( line ) ) !== null ) {
				// ["vt 0.1 0.2", "0.1", "0.2"]
				uvs.push( uv(
					parseFloat( result[ 1 ] ),
					parseFloat( result[ 2 ] )
				) );
			} else if ( ( result = face_pattern1.exec( line ) ) !== null ) {
				// ["f 1 2 3", "1", "2", "3", undefined]
				handle_face_line( [ result[ 1 ], result[ 2 ], result[ 3 ], result[ 4 ] ] );
			} else if ( ( result = face_pattern2.exec( line ) ) !== null ) {
				// ["f 1/1 2/2 3/3", " 1/1", "1", "1", " 2/2", "2", "2", " 3/3", "3", "3", undefined, undefined, undefined]
				handle_face_line(
					[ result[ 2 ], result[ 5 ], result[ 8 ], result[ 11 ] ], //faces
					[ result[ 3 ], result[ 6 ], result[ 9 ], result[ 12 ] ] //uv
				);
			} else if ( ( result = face_pattern3.exec( line ) ) !== null ) {
				// ["f 1/1/1 2/2/2 3/3/3", " 1/1/1", "1", "1", "1", " 2/2/2", "2", "2", "2", " 3/3/3", "3", "3", "3", undefined, undefined, undefined, undefined]
				handle_face_line(
					[ result[ 2 ], result[ 6 ], result[ 10 ], result[ 14 ] ], //faces
					[ result[ 3 ], result[ 7 ], result[ 11 ], result[ 15 ] ], //uv
					[ result[ 4 ], result[ 8 ], result[ 12 ], result[ 16 ] ] //normal
				);
			} else if ( ( result = face_pattern4.exec( line ) ) !== null ) {
				// ["f 1//1 2//2 3//3", " 1//1", "1", "1", " 2//2", "2", "2", " 3//3", "3", "3", undefined, undefined, undefined]
				handle_face_line(
					[ result[ 2 ], result[ 5 ], result[ 8 ], result[ 11 ] ], //faces
					[ ], //uv
					[ result[ 3 ], result[ 6 ], result[ 9 ], result[ 12 ] ] //normal
				);
			} else if ( /^o /.test( line ) ) {
				// object
			} else if ( /^g /.test( line ) ) {
				// group
				//meshN( line.substring( 2 ).trim(), undefined );
				console.log( "THREE.OBJMTLLoader2: grp " + line );
			} else if ( /^usemtl /.test( line ) ) {
				// material
				//line.substring( 7 ).trim()
				mtlCount++;
				// TODO: review the correctness of this
				// not using actual group above
				// adding to groups to avoid TypeError prop 'visible' undefined
				groups.push({ materialIndex: mtlCount - 1 });
				console.log( "THREE.OBJMTLLoader2: mtl " + line );
			} else if ( /^mtllib /.test( line ) ) {
				// mtl file
			} else if ( /^s /.test( line ) ) {
				// Smooth shading
			} else {
				console.log( "THREE.OBJMTLLoader2: Unhandled line " + line );
			}
		}

		if ( vertices.length > 0 ) {
			geometry.vertices = vertices;
			geometry.mergeVertices();
			geometry.computeFaceNormals();
			geometry.computeBoundingSphere();
			geometry.groups = groups;
			object.add( mesh );
		}

		// if ( meshName !== undefined ) {
		// 	console.log("meshname undefined");
		// 	mesh.name = meshName;
		// }

		// if ( materialName !== undefined ) {
		// 	console.log("materialname undefined");
		// 	material = new THREE.MeshLambertMaterial();
		// 	material.name = materialName;
		// 	mesh.material = material;
		// }

		return object;
  }
};
