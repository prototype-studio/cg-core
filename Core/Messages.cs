using Core.Match;

namespace Core
{
    public class Credentials
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
    

    public class AuthenticationSuccessResponse
    {
        public string Token { get; set; }
    }
    
    public class ErrorResponse
    {
        public string ErrorMessage { get; set; }
    }
    
    public abstract class WebSocketMessage {}
    
    public class AuthenticationResult : WebSocketMessage
    {
        public string Username { get; set; }
        public string Error { get; set; }
    }
    
    #region MATCH
    
    public class MatchSessionData : WebSocketMessage
    {
        public string WhiteUsername { get; set; }
        public string BlackUsername { get; set; }
        public int Stake { get; set; }
    }
    
    public class CancelMatchRequest : WebSocketMessage
    {
        
    }
    
    public class OnlineFriendMatchRequest : WebSocketMessage
    {
        public string FriendUsername { get; set; }
    }
    
    public class OnlineRandomMatchRequest : WebSocketMessage
    {
        
    }
    
    public class OnlineRankedMatchRequest : WebSocketMessage
    {
        public int Stake { get; set; }
    }
    
    public class EndOnlineMatchRequest : WebSocketMessage
    {
        
    }
    
    public class OnlineMatchEnded : WebSocketMessage
    {
        
    }
    
    #endregion
}