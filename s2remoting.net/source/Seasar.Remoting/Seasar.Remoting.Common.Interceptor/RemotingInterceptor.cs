using System;
using System.Reflection;
using Seasar.Framework.Aop;
using Seasar.Framework.Aop.Interceptors;
using Seasar.Framework.Container;
using Seasar.Remoting.Common.Connector;
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

namespace Seasar.Remoting.Common.Interceptor
{
	
	/// <summary> �����[�g�I�u�W�F�N�g�̃��\�b�h�Ăяo�����s�����߂̃C���^�[�Z�v�^�ł��B</summary>
	public class RemotingInterceptor : AbstractInterceptor
	{
		protected internal IConnector connector;
		protected internal System.String remoteName;

		/// <summary> �����[�g�I�u�W�F�N�g�̖��O��ݒ肵�܂��B���̃v���p�e�B�̓I�v�V�����ł��B
		/// �R���|�[�l���g��`����擾�ł��閼�O���g�����Ƃ��o���Ȃ��ꍇ�ɂ̂ݐݒ肵�Ă��������B
		/// </summary>
		public string RemoteName
		{
			set { remoteName = value; }
		}

		public virtual IConnector Connector
		{
			set
			{
				this.connector = value;
			}
		}
		
		/// <summary> �^�[�Q�b�g�̃��\�b�h���N�����ꂽ���ɌĂяo����܂��B�N�����ꂽ���\�b�h�����ۃ��\�b�h�Ȃ� {@link Connector}�ɈϏ����܂��B
		/// ��ۃ��\�b�h�Ȃ�^�[�Q�b�g�̃��\�b�h���Ăяo���܂��B
		/// </summary>
		public override object Invoke(IMethodInvocation invocation)
		{
			MethodInfo method = invocation.Method as MethodInfo;
			if (method.IsAbstract || invocation.Target.GetType().IsMarshalByRef)
			{
				return connector.Invoke(GetRemoteName(invocation), method, invocation.Arguments);
			}
			return invocation.Proceed();
		}
		
		/// <summary> �����[�g�I�u�W�F�N�g�̖��O��Ԃ��܂��B�����[�g�I�u�W�F�N�g�̖��O�͎��̏��ŉ������܂��B
		/// <ul>
		/// <li>�v���p�e�B <code>remoteName</code> ���ݒ肳��Ă���΂��̒l�B</li>
		/// <li>�R���|�[�l���g��`�ɖ��O���ݒ肳��Ă���΂��̒l�B</li>
		/// <li>�R���|�[�l���g��`�Ɍ^���ݒ肳��Ă���΂��̖��O����p�b�P�[�W�����������l�B</li>
		/// <li>�^�[�Q�b�g�I�u�W�F�N�g�̌^������p�b�P�[�W�����������l�B</li>
		/// </ul>
		protected internal virtual string GetRemoteName(IMethodInvocation invocation)
		{
			if (remoteName != null)
			{
				return remoteName;
			}
			
			IComponentDef componentDef = GetComponentDef(invocation);
			System.String componentName = componentDef.ComponentName;
			if (componentName != null)
			{
				return componentName;
			}
			
			System.Type componentClass = componentDef.ComponentType;
			if (componentClass != null)
			{
				return componentClass.Name;
			}
			
			return invocation.Target.GetType().Name;
		}
	}
}