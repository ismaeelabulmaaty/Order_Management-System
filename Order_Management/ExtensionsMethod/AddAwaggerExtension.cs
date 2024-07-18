namespace Order_Management.ExtensionsMethod
{
    public static class AddAwaggerExtension
    {
        public static WebApplication UseSwaggerMiddleWare(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();

            return app;
        }

    }
}
