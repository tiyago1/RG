
namespace RiotGamesService
{
public enum ResponseCode
{
    Fail = 0,
    Success = 1,
    Timeout = 2,
    MatchNotFound = 3,
    AlreadyLogined = 4,
    NotAllowed = 5,
    ClientOutDated = 6,
    Cancelled = 7,
    GoogleFail = 8,
    WaitingOpponent = 9,
}

public class ResponseResult<T>
{
    #region Fields

    public T Data { get; set; }
    public ResponseCode Status { get; set; }

    #endregion

    #region Ctors

    public ResponseResult()
    {
        Data = default(T);
        Status = ResponseCode.Success;
    }

    public ResponseResult(T data, ResponseCode status)
    {
        Data = data;
        Status = status;
    }

    public ResponseResult(T data)
    {
        Data = data;
        Status = ResponseCode.Success;
    }

    public ResponseResult(ResponseCode status)
    {
        Status = status;
    }

    #endregion
}
}