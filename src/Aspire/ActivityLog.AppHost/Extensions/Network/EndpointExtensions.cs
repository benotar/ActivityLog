using ActivityLog.Constants.Shared;

namespace ActivityLog.AppHost.Extensions.Network;

public static class EndpointExtensions
{
    public static IResourceBuilder<T> WithHealthCheck<T>(this IResourceBuilder<T> builder)
        where T : ProjectResource
    {
        builder
            .WithUrlForEndpoint(
                "healthchecks",
                url => url.DisplayLocation = UrlDisplayLocation.DetailsOnly
            )
            .WithUrlForEndpoint(SharedConstants.Http, _ =>
                new ResourceUrlAnnotation
                {
                    Url = "/health", DisplayText = "Health Checks", DisplayLocation = UrlDisplayLocation.DetailsOnly
                }
            );

        return builder;
    }

    public static IResourceBuilder<T> WithOpenApi<T>(this IResourceBuilder<T> builder)
        where T : ProjectResource
    {
        if (builder.ApplicationBuilder.ExecutionContext.IsRunMode)
        {
            builder.UpdateHttpAndHttpsEndpoints("Open API ({0})");
        }

        return builder;
    }

    private static void UpdateHttpAndHttpsEndpoints<T>(
        this IResourceBuilder<T> builder,
        string displayTextTemplate
    )
        where T : ProjectResource
    {
        builder.WithUrls(callbackContext =>
            callbackContext.Urls.Where(urlAnnotation =>
                    urlAnnotation.Endpoint?.EndpointName == SharedConstants.Http)
                .ToList()
                .ForEach(urlAnnotation =>
                    urlAnnotation.DisplayText = string.Format(
                        displayTextTemplate,
                        urlAnnotation.Endpoint?.EndpointName.ToUpperInvariant()
                    )
                )
        );
    }
}
