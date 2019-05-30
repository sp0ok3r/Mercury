/*
 ▄▀▀▄ ▄▀▄  ▄▀▀█▄▄▄▄  ▄▀▀▄▀▀▀▄  ▄▀▄▄▄▄   ▄▀▀▄ ▄▀▀▄  ▄▀▀▄▀▀▀▄  ▄▀▀▄ ▀▀▄ 
█  █ ▀  █ ▐  ▄▀   ▐ █   █   █ █ █    ▌ █   █    █ █   █   █ █   ▀▄ ▄▀ 
▐  █    █   █▄▄▄▄▄  ▐  █▀▀█▀  ▐ █      ▐  █    █  ▐  █▀▀█▀  ▐     █   
  █    █    █    ▌   ▄▀    █    █        █    █    ▄▀    █        █   
▄▀   ▄▀    ▄▀▄▄▄▄   █     █    ▄▀▄▄▄▄▀    ▀▄▄▄▄▀  █     █       ▄▀    
█    █     █    ▐   ▐     ▐   █     ▐             ▐     ▐       █     
▐    ▐     ▐                  ▐                                 ▐   
*/
using SteamKit2;
using SteamKit2.Internal;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace MercuryBOT.CallbackMessages
{
    //Thanks to Paul "Xetas" Abramov
    public class PurchaseResponseCallback : CallbackMsg
    {
        public Dictionary<uint, string> m_Items = new Dictionary<uint, string>();


        public EPurchaseResultDetail m_PurchaseResultDetail;
        public EResult m_Result;

        public PurchaseResponseCallback(JobID _jobID, CMsgClientPurchaseResponse _clientPurchaseMessage)
        {
            JobID = _jobID;
            m_PurchaseResultDetail = (EPurchaseResultDetail)_clientPurchaseMessage.purchase_result_details;
            m_Result = (EResult)_clientPurchaseMessage.eresult;

            KeyValue receiptInfo = new KeyValue();
            using (MemoryStream memoryStream = new MemoryStream(_clientPurchaseMessage.purchase_receipt_info))
            {
                if (!receiptInfo.TryReadAsBinary(memoryStream))
                {
                    return;
                }
            }

            List<KeyValue> lineItems = receiptInfo["lineitems"].Children;

            foreach (KeyValue lineItem in lineItems)
            {
                uint packageID = lineItem["PackageID"].AsUnsignedInteger();
                if (packageID == 0)
                {
                    packageID = lineItem["ItemAppID"].AsUnsignedInteger();
                    if (packageID == 0)
                    {
                        return;
                    }
                }

                string gameName = lineItem["ItemDescription"].Value;
                if (string.IsNullOrEmpty(gameName))
                {
                    return;
                }

                gameName = WebUtility.HtmlDecode(gameName);
                m_Items[packageID] = gameName;
            }
        }
    }
}