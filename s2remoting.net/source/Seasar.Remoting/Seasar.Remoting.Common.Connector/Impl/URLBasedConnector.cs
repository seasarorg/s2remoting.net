/*
* Copyright 2004-2006 the Seasar Foundation and the Others.
*
* Licensed under the Apache License, Version 2.0 (the "License");
* you may not use this file except in compliance with the License.
* You may obtain a copy of the License at
*
*     http://www.apache.org/licenses/LICENSE-2.0
*
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, 
* either express or implied. See the License for the specific language
* governing permissions and limitations under the License.
*/
using System;
using System.Reflection;

namespace Seasar.Remoting.Common.Connector.Impl
{
	
	/// <summary> URL�Ɋ�Â��ă����[�g���\�b�h�Ăяo�����s���R�l�N�^�̒��ۊ��N���X�ł��B
	/// 
	/// </summary>
	public abstract class URLBasedConnector : IConnector
	{
		private System.Uri baseURL;
		/// <summary>
		///  �x�[�XURL��Ԃ��܂��B
		/// </summary>
		/// <returns>
		///  �x�[�XURL
		/// </returns>
		/// <summary> 
		/// �x�[�XURL��ݒ肵�܂��B
		/// </summary>
		public System.Uri BaseURL
		{
			get
			{
				return baseURL;
			}
			
			set
			{
				this.baseURL = value;
			}
			
		}
		
		public abstract object Invoke(string param1, MethodInfo param2, object[] param3);
	}
}