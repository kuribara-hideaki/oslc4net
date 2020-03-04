/*******************************************************************************
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
 *     Steve Pitschke  - initial API and implementation
 *******************************************************************************/

using System.Collections.Generic;
using System.Linq;
using Antlr.Runtime.Tree;

namespace OSLC4Net.Core.Query.Impl
{
    /// <summary>
    /// Implementation of SelectClause interface
    /// </summary>
    internal class SelectClauseImpl : PropertiesImpl, SelectClause
    {
        public
        SelectClauseImpl(CommonTree tree = null, IReadOnlyDictionary<string, string> prefixMap = null,
            bool isError = false, string errorReason = null)
            : base(tree, prefixMap)
        {
            IsError = isError || (this.Children?.Any(item => item.IsError) ?? false);
            ErrorReason = errorReason;
        }

        public bool IsError { get; }
        public string ErrorReason { get; }
    }
}
