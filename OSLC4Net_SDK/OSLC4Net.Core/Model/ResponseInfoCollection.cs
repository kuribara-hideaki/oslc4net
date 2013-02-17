﻿/*******************************************************************************
 * Copyright (c) 2013 IBM Corporation.
 *
 * All rights reserved. This program and the accompanying materials
 * are made available under the terms of the Eclipse Public License v1.0
 * and Eclipse Distribution License v. 1.0 which accompanies this distribution.
 *  
 * The Eclipse Public License is available at http://www.eclipse.org/legal/epl-v10.html
 * and the Eclipse Distribution License is available at
 * http://www.eclipse.org/org/documents/edl-v10.php.
 *
 * Contributors:
 *
 *     Steve Pitschke  - initial API and implementation
 *******************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OSLC4Net.Core.Attribute;

namespace OSLC4Net.Core.Model
{
    /// <summary>
    /// An OSLC ResponseInfo resource containg an IEnumerable collection of member resources
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ResponseInfoCollection<T> : ResponseInfo<ICollection<T>>
    {
        /**
         * Collection of resources
         */
        public ICollection<T> Collection() { return Resource; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="properties"></param>
        /// <param name="totalCount"></param>
        /// <param name="nextPage"></param>
        public
        ResponseInfoCollection(
            ICollection<T> collection,
            IDictionary<String, Object> properties,
            int totalCount,
            String nextPage
        ) : base(collection, properties, totalCount, nextPage)
        {
        }
    
        /// <summary>
        /// 
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="properties"></param>
        /// <param name="totalCount"></param>
        /// <param name="nextPage"></param>
        public
        ResponseInfoCollection(
            ICollection<T> collection,
            IDictionary<String, Object> properties,
            int totalCount,
            Uri nextPage
        ) : base(collection, properties, totalCount, nextPage)
        {
        }
    }
}
