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
using System.Collections;
using System.Reflection;

namespace Seasar.Remoting.Common.Connector.Impl
{
	
	public abstract class TargetSpecificURLBasedConnector : URLBasedConnector
	{
		/// <summary> リモートオブジェクトのURLをキャッシュする上限のデフォルト値</summary>
		protected internal const int DEFAULT_MAX_CACHED_URLS = 10;
		
		private LruCache cachedURLs;

        protected TargetSpecificURLBasedConnector()
		{
			cachedURLs = new LruCache(DEFAULT_MAX_CACHED_URLS);
		}

		public virtual int MaxCachedURLs
		{
			set
			{
				lock (this)
				{
					cachedURLs.CacheSize = value;
				}
			}
		}
	
		public override object Invoke(string remoteName, MethodInfo method, object[] args)
		{
			return Invoke(GetTargetURL(remoteName), method, args);
		}
		
		public virtual Uri GetTargetURL(string remoteName)
		{
			lock (this)
			{
				System.Uri targetURL = null;
				if ( cachedURLs.Contains(remoteName) )
					targetURL = cachedURLs[remoteName] as System.Uri;

				if (targetURL == null)
				{
					targetURL = new System.Uri(base.BaseURL, remoteName);
					cachedURLs[remoteName] = targetURL;
				}
				return targetURL;
			}
		}
		
		public abstract object Invoke(Uri targetURL, MethodInfo method, object[] args);

		public class LruCache
		{
			private const int DEFAULT_CACHE_SIZE = 100;
			private int cacheSize;
			private Hashtable cache;
			private IList keyList;

			public LruCache() 
			{
				this.cacheSize = DEFAULT_CACHE_SIZE;
				this.cache = Hashtable.Synchronized( new Hashtable() );
				this.keyList = ArrayList.Synchronized( new ArrayList() );
			}

			public LruCache(int cacheSize) 
			{
				this.cacheSize = cacheSize;
				this.cache = Hashtable.Synchronized( new Hashtable() );
				this.keyList = ArrayList.Synchronized( new ArrayList() );
			}

			public int Count
			{
				get { return this.cache.Count; }
			}

			public int CacheSize
			{
				set
				{
					if (this.cache.Count > value)
						throw new ArgumentException(
							"割当て済みキャッシュのサイズより少ないCacheSizeは指定できません"
						);
					this.cacheSize = value;
				}
			}

			public object Remove(object key)
			{
				object o = this[key];

				this.keyList.Remove(key);
				this.cache.Remove(key);
				return o;
			}

			public void Clear()
			{
				this.cache.Clear();
				this.keyList.Clear();	
			}

			public bool Contains(object key)
			{
				return this.cache.Contains(key);
			}

			public object this[object key] 
			{
				get
				{
					this.keyList.Remove(key);
					this.keyList.Add(key);
					return this.cache[key];
				}
				set
				{
					this.cache[key] = value;
					this.keyList.Add(key);
					if (this.keyList.Count > this.cacheSize) 
					{
						object oldestKey = this.keyList[0];
						this.keyList.RemoveAt(0);
						this.cache.Remove(oldestKey);
					}		
				}
			}
		}		
	}
}