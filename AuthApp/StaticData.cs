namespace AuthApp
{
    public class StaticData
    {
        public const string AuthAPIBase = "https://petstore.swagger.io/v2";
        public const string ViewInfoUnauthorize = "Авторизуйтесь или зарегистрируйтесь\rдля просмотра информации пользователя";
        public enum ApiTypes
        {
            GET,
            POST,
            PUT,
            DELETE
        }
    }
}