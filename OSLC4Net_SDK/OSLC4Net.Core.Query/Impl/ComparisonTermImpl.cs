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
    /// implmentation of ComparisonTerm interface
    /// </summary>
    internal class ComparisonTermImpl : SimpleTermImpl, ComparisonTerm
    {
        public ComparisonTermImpl(CommonTree tree, IReadOnlyDictionary<string, string> prefixMap) : base(tree, TermType.COMPARISON, prefixMap)
        {
            switch (((CommonTree)tree.GetChild(1)).Token.Type)
            {
                case OslcWhereParser.EQUAL:
                    Operator = Operator.EQUALS;
                    break;

                case OslcWhereParser.NOT_EQUAL:
                    Operator = Operator.NOT_EQUALS;
                    break;

                case OslcWhereParser.LESS:
                    Operator = Operator.LESS_THAN;
                    break;

                case OslcWhereParser.LESS_EQUAL:
                    Operator = Operator.LESS_EQUALS;
                    break;

                case OslcWhereParser.GREATER:
                    Operator = Operator.GREATER_THAN;
                    break;

                default:
                case OslcWhereParser.GREATER_EQUAL:
                    Operator = Operator.GREATER_EQUALS;
                    break;
            }

            MakeOperand();
        }

        public Operator Operator { get; }

        private void MakeOperand()
        {
            if (tree?.ChildCount >= 3)
            {
                var treeOperand = (CommonTree)tree.GetChild(2);
                if (treeOperand is Antlr.Runtime.Tree.CommonErrorNode errorNode)
                {
                    isError = true;
                    errorReason = errorNode.trappedException.ToString();
                    return;
                }
                if (treeOperand == null)
                {
                    isError = true;
                    errorReason = "operand is null";
                    return;
                }
                var reason = "unsupported literal value type";
                Operand = CreateValue(treeOperand, reason, prefixMap);
                if (Operand is ValueImpl withError && withError.IsError)
                {
                    isError = true;
                    errorReason = withError.ErrorReason;
                }
            }
        }

        public IValue Operand { get; private set; }

        public override bool IsError => isError;

        public override string ErrorReason => errorReason;

        public override string ToString()
        {
            return Operand == null ? string.Empty : Property.ToString() + OperatorExtension.ToString(Operator) + Operand.ToString();
        }

        internal static IValue CreateValue(CommonTree treeOperand, string errorPrefix, IReadOnlyDictionary<string, string> prefixMap)
        {
            switch (treeOperand.Token.Type)
            {
                case OslcWhereParser.IRI_REF:
                    return new UriRefValueImpl(treeOperand);

                case OslcWhereParser.BOOLEAN:
                    return new BooleanValueImpl(treeOperand);

                case OslcWhereParser.DECIMAL:
                    return new DecimalValueImpl(treeOperand);

                case OslcWhereParser.STRING_LITERAL:
                case OslcWhereParser.ASTERISK:
                    return new StringValueImpl(treeOperand);

                case OslcWhereParser.TYPED_VALUE:
                    return new TypedValueImpl(treeOperand, prefixMap);

                case OslcWhereParser.LANGED_VALUE:
                    return new LangedStringValueImpl(treeOperand);

                default:
                    return new ValueImpl(isError: true, errorReason: errorPrefix + ": " + treeOperand.Token.Text);
            }
        }
    }

    internal static class OperatorExtension
    {
        public static string
        ToString(Operator op)
        {
            switch (op)
            {
                case Operator.EQUALS:
                    return "=";

                case Operator.NOT_EQUALS:
                    return "!=";

                case Operator.LESS_THAN:
                    return "<";

                case Operator.GREATER_THAN:
                    return ">";

                case Operator.LESS_EQUALS:
                    return "<=";

                default:
                case Operator.GREATER_EQUALS:
                    return ">=";
            }
        }
    }
}
