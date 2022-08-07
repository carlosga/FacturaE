// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace ASN1;

public interface IAsnValueObject<T> : IAsnValueObject
{
    new T Value { get; }
}
