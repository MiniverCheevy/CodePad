using ClientGenerator.Models;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace ClientGenerator.Helpers
{
    public class EndPointFactory
    {
        private Api api;

        public List<EndPoint> GetEndPoints(Api api)
        {
            this.api = api;
            var EndPoints = new List<EndPoint>();
            var methods = api.Type.GetMethods().ToArray();
            foreach (var method in methods)
            {
                var EndPoint = GetEndPoint(method);
                if (EndPoint != null)
                    EndPoints.Add(EndPoint);
            }
            var names = EndPoints.Select(c => c.Method.Name).GroupBy(c => c).ToArray();
            var multiple = false;
            Dictionary<string, int> nameCount = new Dictionary<string, int>();
            if (names.Any(c => c.Count() > 0))
                multiple = true;

            foreach (var EndPoint in EndPoints)
            {
                var name = EndPoint.Method.Name;
                EndPoint.Name = name;
                if (multiple)
                {
                    //TODO: come up with a better naming scheme;
                    if (!nameCount.ContainsKey(name))
                    {                       
                        nameCount.Add(name, 1);
                    }
                    else
                    {
                        var count = nameCount[name]++;
                        EndPoint.Name = $"{name}{count}";
                    }
                }                
            }

            return EndPoints;
        }


        private EndPoint? GetEndPoint(MethodInfo method)
        {
            var get = method.GetCustomAttribute<HttpGetAttribute>();
            if (get != null)
            {
                return new EndPoint(api, method) { Verb = Verb.Get, Attribute = get };
            }
            if (method.Name == "Get")
            {
                return new EndPoint(api, method) { Verb = Verb.Get, Api = api };
            }
            var post = method.GetCustomAttribute<HttpPostAttribute>();
            if (post != null)
            {
                return new EndPoint(api, method) { Verb = Verb.Post, Attribute = post };
            }
            if (method.Name == "Post")
            {
                return new EndPoint(api, method) { Verb = Verb.Post, Api = api };
            }
            var put = method.GetCustomAttribute<HttpPutAttribute>();
            if (put != null)
            {
                return new EndPoint(api, method) { Verb = Verb.Put, Attribute = put };
            }
            if (method.Name == "Put")
            {
                return new EndPoint(api, method) { Verb = Verb.Put, Api = api };
            }
            var delete = method.GetCustomAttribute<HttpDeleteAttribute>();
            if (delete != null)
            {
                return new EndPoint(api, method) { Verb = Verb.Delete, Attribute = delete };
            }
            if (method.Name == "Delete")
            {
                return new EndPoint(api, method) { Verb = Verb.Delete, Api = api };
            }
            var patch = method.GetCustomAttribute<HttpPatchAttribute>();
            if (patch != null)
            {
                return new EndPoint(api, method) { Verb = Verb.Patch, Attribute = patch };
            }
            if (method.Name == "Patch")
            {
                return new EndPoint(api, method) { Verb = Verb.Patch, Api = api };
            }
            return null;
        }
    }
}
