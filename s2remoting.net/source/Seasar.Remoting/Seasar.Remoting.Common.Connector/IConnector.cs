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

namespace Seasar.Remoting.Common.Connector
{
	
	/// <summary> 
	/// �����[�g�I�u�W�F�N�g�ɑ΂��郁�\�b�h�Ăяo�������s����I�u�W�F�N�g����������C���^�t�F�[�X�ł��B
	/// �����N���X�͌ŗL�̕��@(�v���g�R��)�Ń����[�g�I�u�W�F�N�g�̃��\�b�h���Ăяo���܂��B
	/// </summary>
	public interface IConnector
	{
        object Invoke(string remoteName, MethodInfo method, object[] args);
	}
}