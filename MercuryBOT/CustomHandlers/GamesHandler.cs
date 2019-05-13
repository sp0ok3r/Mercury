using MercuryBOT.CallbackMessages;
using SteamKit2;
using SteamKit2.Internal;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MercuryBOT.CustomHandlers
{
    //Thanks to Paul "Xetas" Abramov
    public class GamesHandler : ClientMsgHandler
    {
        public override void HandleMsg(IPacketMsg _packetMsg)
        {
            switch (_packetMsg.MsgType)
            {
                case EMsg.ClientPurchaseResponse:
                    HandlePurchaseResponse(_packetMsg);
                    break;
            }
        }
        public void SetGamePlayingNormal(int _gameID)
        {
            ClientMsgProtobuf<CMsgClientGamesPlayed> gamePlaying = new ClientMsgProtobuf<CMsgClientGamesPlayed>(EMsg.ClientGamesPlayed);

            gamePlaying.Body.games_played.Add(new CMsgClientGamesPlayed.GamePlayed { game_id = new GameID(_gameID) });

            Client.Send(gamePlaying);
        }

        public void SetGamePlayingNONSteam(string _game)
        {
            ClientMsgProtobuf<CMsgClientGamesPlayed> gamePlaying = new ClientMsgProtobuf<CMsgClientGamesPlayed>(EMsg.ClientGamesPlayed);
            gamePlaying.Body.games_played.Add(new CMsgClientGamesPlayed.GamePlayed { game_id = 12350489788975939584,game_extra_info= _game });
            Client.Send(gamePlaying);
        }

        public void StopPlayingGames()
        {
            ClientMsgProtobuf<CMsgClientGamesPlayed> request = new ClientMsgProtobuf<CMsgClientGamesPlayed>(EMsg.ClientGamesPlayed);
            request.Body.games_played.Add(new CMsgClientGamesPlayed.GamePlayed { game_id = new GameID(0) });
            Client.Send(request);
        }


        public async Task<PurchaseResponseCallback> RedeemKey(string _keyToActivate)
        {
            ClientMsgProtobuf<CMsgClientRegisterKey> registerKey = new ClientMsgProtobuf<CMsgClientRegisterKey>(EMsg.ClientRegisterKey)
            {
                SourceJobID = Client.GetNextJobID()
            };

            registerKey.Body.key = _keyToActivate;

            Client.Send(registerKey);

            try
            {
                return await new AsyncJob<PurchaseResponseCallback>(Client, registerKey.SourceJobID);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }


        public async Task<string> RedeemKeyResponse(string _keyToActivate)
        {
            PurchaseResponseCallback activatedResponse = await RedeemKey(_keyToActivate).ConfigureAwait(false);

            return $"Status: {activatedResponse.m_Result}/{activatedResponse.m_PurchaseResultDetail}, {string.Join(",", activatedResponse.m_Items.Select(_key => $"Key: [ {_keyToActivate} ] Game: [ {_key.Key}/{_key.Value} ]").ToArray())}";
        }


        private void HandlePurchaseResponse(IPacketMsg _packetMsg)
        {
            if (_packetMsg == null)
            {
                return;
            }

            ClientMsgProtobuf<CMsgClientPurchaseResponse> response = new ClientMsgProtobuf<CMsgClientPurchaseResponse>(_packetMsg);
            Client.PostCallback(new PurchaseResponseCallback(_packetMsg.TargetJobID, response.Body));
        }
    }
}