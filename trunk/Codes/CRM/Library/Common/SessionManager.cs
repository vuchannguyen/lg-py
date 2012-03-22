using System;
using System.Web;
using System.Web.SessionState;

namespace CRM.Library.Common
{
    public class SessionManager
    {
        /// <summary>
        /// Add
        /// </summary>
        /// <param name="context"></param>
        /// <param name="sessionState"></param>
        public static void Add(HttpSessionStateBase sessionState, string sessionKey, object value)
        {
            sessionState.Add(sessionKey, value);
        }

        /// <summary>
        /// Set Timeout
        /// </summary>
        /// <param name="context"></param>
        /// <param name="sessionState"></param>
        public static void SetTimeout(HttpSessionStateBase sessionState, int timeout)
        {
            sessionState.Timeout = timeout;
        }

        /// <summary>
        /// Get
        /// </summary>
        /// <param name="context"></param>
        /// <param name="sessionState"></param>
        public static object Get(HttpSessionStateBase sessionState, string sessionKey)
        {
            if (sessionState[sessionKey] != null)
                return sessionState[sessionKey];
            return null;
        }

        /// <summary>
        /// Get Auto
        /// </summary>
        /// <param name="context"></param>
        /// <param name="sessionState"></param>
        public static object GetAuto(HttpSessionStateBase sessionState, string sessionKey, object value)
        {
            if (sessionState[sessionKey] != null)
                return sessionState[sessionKey];
            //else
            //return to LoginPage
            return null;
        }

        /// <summary>
        /// Remove
        /// </summary>
        /// <param name="context"></param>
        /// <param name="sessionState"></param>
        public static void Remove(HttpSessionStateBase sessionState, string sessionKey)
        {
            sessionState.Remove(sessionKey);
        }

        /// <summary>
        /// Destroy
        /// </summary>
        /// <param name="sessionState"></param>
        public static void Destroy(HttpSessionStateBase sessionState)
        {
            sessionState.Abandon();
        }

        /// <summary>
        /// Clear All
        /// </summary>
        /// <param name="sessionState"></param>
        public static void ClearAll(HttpSessionStateBase sessionState)
        {
            sessionState.Clear();
        }
    }
}
