using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuddhaGame
{
	public class HttpRouteRequest : HttpRequest
	{
		public HttpRouteRequest(string controller, string filename) : base(controller + filename)
        {

        }
	}
}
