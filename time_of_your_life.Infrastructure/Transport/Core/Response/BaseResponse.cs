namespace time_of_your_life.Infrastructure.Transport.Core.Response
{ 
    public class BaseResponse
    {
        public BaseStateResponse State { get; set; }

        public BaseResponse()
        {
            State = new BaseStateResponse();
        }

        public static BaseResponse Complete(bool isOk)
        {
            return new BaseResponse { State = new BaseStateResponse { HasError = !isOk } };
        }

        public static BaseResponse Ok()
        {
            return new BaseResponse{ State = new BaseStateResponse{ HasError = false }};
        }

        public static BaseResponse Ok(string message)
        {
            return new BaseResponse{ State = new BaseStateResponse{ HasError = false, MensajeDetalle =  message }};
        }

        public static BaseResponse Failed(string message)
        {
            return new BaseResponse{ State = new BaseStateResponse{ HasError = true, MensajeError =  message }};
        }
    }

    public class BaseResponse<TResult> where TResult : class, new()
    {
        public BaseStateResponse State { get; set; }
        public TResult Result { get; set; }

        public BaseResponse()
        {
            State = new BaseStateResponse();
            Result = new TResult();
        }

        public BaseResponse<TResult> Correct()
        {
            this.State.HasError = false;
            this.State.MensajeError = null;
            return this;
        }

        public BaseResponse<TResult> Correct(TResult result)
        {
            this.Result = result;
            return this;
        }

        public BaseResponse<TResult> Correct(string message)
        {
            this.State.HasError = false;
            this.State.MensajeError = message;
            return this;
        }

        public BaseResponse<TResult> Fail(string message)
        {
            this.State.HasError = true;
            this.State.MensajeError = message;
            return this;
        }

        public static BaseResponse<TResult> Complete(bool isOk)
        {
            return new BaseResponse<TResult> { State = new BaseStateResponse { HasError = !isOk } };
        }

        public static BaseResponse<TResult> Complete(TResult result)
        {
            return new BaseResponse<TResult> { State = new BaseStateResponse { HasError = false }, Result = result };
        }

        public static BaseResponse<TResult> Ok()
        {
            return new BaseResponse<TResult> { State = new BaseStateResponse { HasError = false } };
        }

        public static BaseResponse<TResult> Ok(TResult result)
        {
            return new BaseResponse<TResult> { State = new BaseStateResponse { HasError = false }, Result = result };
        }

        public static BaseResponse<TResult> Ok(string message)
        {
            return new BaseResponse<TResult> { State = new BaseStateResponse { HasError = false, MensajeError = message } };
        }

        public static BaseResponse<TResult> Failed(string message)
        {
            return new BaseResponse<TResult> { State = new BaseStateResponse { HasError = true, MensajeError = message } };
        }
    }
}
