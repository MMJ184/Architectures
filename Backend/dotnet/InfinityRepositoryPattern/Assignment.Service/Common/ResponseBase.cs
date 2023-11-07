namespace Assignment.Service.Common
{
    public class ResponseBase<TModel> : ResponseBaseModel
    {
        public ResponseBase(TModel model)
        {
            Result = model;
        }

        public TModel Result { get; set; }
    }
}
