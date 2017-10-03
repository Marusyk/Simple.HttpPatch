<p align="center">
  <img src="HttpPatch.png" alt="HttpPatch" width="250"/>
</p>

# WebAPI.Patch

WebAPI.HttpPatch implementation for .NET to easily allow & apply partial REST-ful service (through Web API) using Http Patch


To supprot `Http.Patch` in RESTful API you can use:

 - [OData Delta](http://www.odata.org/)
 - [Json Patch](http://jsonpatch.com/)
 
 *So, what is the advantage of WebAPI.HttpPatch?* <br/>
 WebAPI.HttpPatch is simple, lightweight package that bring `Http.Patch` into your RESTful application.
 
 OData cannot work with `int` and `guid` types.  
 Json Patch is a little bit complicated for simple update a few properties
 
 Instead of this:
 
 ```
[
  { "op": "replace", "path": "/name", "value": "foo" },
  { "op": "replace", "path": "/age", "value": "25" }
]
```
you can use:
```
{
  "Name" : "foo",
  "Age": 25
}
 ```
 
