// This script is placed in public domain. The author takes no responsibility for any possible harm.

var scale = 10.0;
var speed = 1.0;
private var baseHeight : Vector3[];
var useOriginal : boolean = false;

//var noiseStrength = 4.0;
//var noiseWalk = 1.0;

function FixedUpdate () {
	 /*var mesh : Mesh = GetComponent(MeshFilter).mesh;

        if (baseHeight == null)
                baseHeight = mesh.vertices;

        var vertices = new Vector3[baseHeight.Length];
        for (var i=0;i<vertices.Length;i++)
        {
                var vertex = baseHeight[i];
                vertex.y += Mathf.Sin(Time.time * speed+ baseHeight[i].x + baseHeight[i].y + baseHeight[i].z) * scale;
                vertex.y += Mathf.PerlinNoise(baseHeight[i].x + noiseWalk, baseHeight[i].y + Mathf.Sin(Time.time * 0.1) ) * noiseStrength;
                vertices[i] = vertex;
        }
        mesh.vertices = vertices;
        mesh.RecalculateNormals();*/

	var mesh : Mesh = GetComponent(MeshFilter).mesh;
	
	if (baseHeight == null)
		baseHeight = mesh.vertices;
	
	// gameObject.Destroy(GetComponent(MeshCollider));
	
	var vertices = new Vector3[baseHeight.Length];
	for (var i=0;i<vertices.Length;i++)
	{
		var vertex = baseHeight[i];
		
		//if (Vector3.Distance(Camera.main.transform.position, baseHeight[i]) < 20)
		//{
		
			if (useOriginal) {
				vertex.y += Mathf.Sin(Time.time * speed+ baseHeight[i].x + baseHeight[i].y + baseHeight[i].z) * scale;
			} else {
				vertex.y += Mathf.Sin(Time.time * speed+ baseHeight[i].x + baseHeight[i].y) * (scale*.5) + Mathf.Sin(Time.time * speed+ baseHeight[i].z + baseHeight[i].y) * (scale*.5);
			}
		//}
		
		vertices[i] = vertex;
	}
	mesh.vertices = vertices;
	mesh.RecalculateNormals();
	
	gameObject.Destroy(GetComponent(MeshCollider));
	
	var collider : MeshCollider = GetComponent(MeshCollider);
	if (collider == null) {
		collider = gameObject.AddComponent(MeshCollider);
		collider.isTrigger = true;
	}
	
}

