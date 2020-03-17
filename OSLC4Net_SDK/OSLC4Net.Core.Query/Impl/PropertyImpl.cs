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
    /// <summary>
    /// Implementation of Property interface
    /// </summary>
    internal class PropertyImpl : Property
    {
        public PropertyImpl(CommonTree tree, PropertyType type, IReadOnlyDictionary<string, string> prefixMap, bool isWildcard)
        {
            this.tree = tree;
            this.Type = type;
            this.prefixMap = prefixMap;
            this.IsWildcard = isWildcard;
            if (tree != null)
            {
                MakeIdentifier();
            }
        }

        public PropertyType Type { get; }

        public bool IsWildcard { get; }

        private void MakeIdentifier()
        {
            if (identifier != null)
            {
                return;
            }

            if (IsWildcard)
            {
                errorReason = "wildcard has no identifier";
                isError = true;
                return;
            }

            string rawIdentifier = tree.Text;

            identifier = new PName();

            int colon = rawIdentifier.IndexOf(':');

            if (colon < 0)
            {
                identifier.local = rawIdentifier;
            }
            else
            {
                if (colon > 0)
                {
                    identifier.prefix = rawIdentifier.Substring(0, colon);
                    identifier.ns = prefixMap[identifier.prefix];
                }
                identifier.local = rawIdentifier.Substring(colon + 1);
            }
        }

        public PName Identifier => identifier;

        public bool IsError => isError;
        private bool isError;

        public string ErrorReason => errorReason;
        private string errorReason;

        public override string ToString()
        {
            return Identifier.ToString();
        }

        private readonly CommonTree tree;
        protected readonly IReadOnlyDictionary<string, string> prefixMap;
        private PName identifier = null;
    }
}
