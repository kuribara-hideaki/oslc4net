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

using Antlr.Runtime.Tree;

namespace OSLC4Net.Core.Query.Impl
{
    internal class SortTermImpl : SortTerm
    {
        public SortTermImpl(SortTermType type, CommonTree tree, IReadOnlyDictionary<string, string> prefixMap)
        {
            this.Type = type;
            this.tree = tree;
            this.prefixMap = prefixMap;
        }

        public SortTermType Type { get; }

        public PName
        Identifier
        {
            get
            {
                if (identifier != null)
                {
                    return identifier;
                }

                string rawProperty = tree.GetChild(0).Text;

                identifier = new PName();

                int colon = rawProperty.IndexOf(':');

                if (colon < 0)
                {
                    identifier.local = rawProperty;
                }
                else
                {
                    if (colon > 0)
                    {
                        identifier.prefix = rawProperty.Substring(0, colon);
                        identifier.ns = prefixMap[identifier.prefix];
                    }
                    identifier.local = rawProperty.Substring(colon + 1);
                }

                return identifier;
            }
        }

        protected readonly CommonTree tree;
        protected readonly IReadOnlyDictionary<string, string> prefixMap;
        private PName identifier = null;
    }
}
