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
namespace Seasar.Remoting.Common.Deployer
{
	
	/// <summary> 
	/// �R���|�[�l���g�������[�g�I�u�W�F�N�g�Ƃ��ăf�v���C���邽�߂̃C���^�t�F�[�X�ł��B
	/// </summary>
	public interface IDeployer
	{
		
		/// <summary> 
		/// �R���|�[�l���g�������[�g�I�u�W�F�N�g�Ƃ��ăf�v���C���܂��B
		/// </summary>
		void  Deploy();
	}
}