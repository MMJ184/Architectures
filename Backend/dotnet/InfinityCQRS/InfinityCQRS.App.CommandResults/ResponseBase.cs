namespace InfinityCQRS.App.CommandResults
{
    public class ResponseBase<TModel> : ResponseBaseModel
    {
        public ResponseBase(TModel model)
        {
            Result = model;
        }

        public TModel Result { get; set; }

        public long Count { get; set; }
    }
}
