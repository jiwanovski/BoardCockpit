﻿using System.Xml;
using System.Collections.Generic;
using System.Linq;

namespace JeffFerguson.Gepsio
{
    /// <summary>
    /// An encapsulation of the XBRL element "labelLink" as defined in the http://www.xbrl.org/2003/linkbase namespace. 
    /// </summary>
    public class LabelLink
    {
        /// <summary>
        /// A collection of <see cref="Locator"/> objects for the label link.
        /// </summary>
        public List<Locator> Locators
        {
            get;
            private set;
        }

        /// <summary>
        /// A collection of <see cref="LabelArc"/> objects for the label link.
        /// </summary>
        public List<LabelArc> LabelArcs
        {
            get;
            private set;
        }

        /// <summary>
        /// A collection of <see cref="Label"/> objects for the label link.
        /// </summary>
        public List<Label> Labels
        {
            get;
            private set;
        }

        //------------------------------------------------------------------------------------
        //------------------------------------------------------------------------------------
        internal LabelLink(XmlNode LabelLinkNode)
        {
            this.Locators = new List<Locator>();
            this.LabelArcs = new List<LabelArc>();
            this.Labels = new List<Label>();
            ReadChildLocators(LabelLinkNode);
            ReadChildLabelArcs(LabelLinkNode);
            ReadChildLabels(LabelLinkNode);
            ResolveLocators();
        }

        //------------------------------------------------------------------------------------
        //------------------------------------------------------------------------------------
        private void ResolveLocators()
        {
            IDictionary<string, Locator> dictLocators = this.Locators.ToDictionary(loc => loc.Label);
            foreach (LabelArc CurrentLabelArc in this.LabelArcs)
            {
                try
                {
                    CurrentLabelArc.FromLocator = dictLocators[CurrentLabelArc.FromId];
                }
                catch(KeyNotFoundException)
                {
                    CurrentLabelArc.FromLocator = null;
                }
            }
        }

        //------------------------------------------------------------------------------------
        //------------------------------------------------------------------------------------
        private void ReadChildLabelArcs(XmlNode LabelLinkNode)
        {
            foreach (XmlNode CurrentChildNode in LabelLinkNode.ChildNodes)
            {
                if (CurrentChildNode.LocalName.Equals("labelArc") == true)
                    this.LabelArcs.Add(new LabelArc(CurrentChildNode));
            }
        }

        //------------------------------------------------------------------------------------
        //------------------------------------------------------------------------------------
        private void ReadChildLocators(XmlNode LabelLinkNode)
        {
            foreach (XmlNode CurrentChildNode in LabelLinkNode.ChildNodes)
            {
                if (CurrentChildNode.LocalName.Equals("loc") == true)
                    this.Locators.Add(new Locator(CurrentChildNode));
            }
        }

        //------------------------------------------------------------------------------------
        //------------------------------------------------------------------------------------
        private void ReadChildLabels(XmlNode LabelLinkNode)
        {
            foreach (XmlNode CurrentChildNode in LabelLinkNode.ChildNodes)
            {
                if (CurrentChildNode.LocalName.Equals("label") == true)
                    this.Labels.Add(new Label(CurrentChildNode));
            }
        }
    }
}
