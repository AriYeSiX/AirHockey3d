using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DeviceInfos
{
    /// <summary>
    /// Realisation to get device info
    /// </summary>
    public class DeviceInfo
    {
        private const string GOOGLE = "google";
        private const string TELEPHONY_MANAGER = "android.telephony.TelephonyManager";
        private const string GET_SIM_FUNC = "getSimState";
        
        /// <summary>
        /// Check contains device name "goggle" or not
        /// </summary>
        /// <returns>true if device name contain "google"</returns>
        public bool IsGoogleDevice() =>
            GetBrand().Contains((GOOGLE), StringComparison.InvariantCultureIgnoreCase);
        
        private string GetBrand() =>
            SystemInfo.deviceModel;
        
        /// <summary>
        /// Check have phone sim card or not
        /// </summary>
        /// <returns>true if have sim card</returns>
        public bool GetSimState()
        {
            AndroidJavaObject TelephonyManager = new AndroidJavaObject(TELEPHONY_MANAGER);
            int simState = TelephonyManager.Call<int>(GET_SIM_FUNC);

            return simState switch
            {
                0 => false,
                1 => false,
                _ => true
            };
        }
    }
}


