        private string GetCurrentUrl()
        {
            var context = this.Request.RequestContext;
            var routeParams = MvcRequestContextBuilder.GetRouteParams(context);
            var url = RouteHelper.GetUrlParameterString(routeParams);
            return url;
        }
