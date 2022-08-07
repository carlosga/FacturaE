// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Xml.Serialization;

namespace FacturaE.XAdES;

/// <summary>
/// Rol del "firmante" de la factura
/// </summary>
public enum ClaimedRole
{
    /// <summary>
    ///  “emisor”: cuando la firma de la factura la realiza el emisor.
    /// </summary>
    [XmlEnum("supplier")]
    Supplier,
    /// <summary>
    /// cuando la firma de la factura la realiza el receptor.
    /// </summary>
    [XmlEnum("customer")]
    Customer,
    /// <summary>
    /// Cuando la firma la realiza una persona o entidad distinta al emisor o al receptor de la factura.
    /// </summary>
    [XmlEnum("third party")]
    ThirdParty
}