// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Formatting.DataSets;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.TestCommon;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace System.Net.Http.Formatting
{
    public class BsonMediaTypeFormatterTests // : MediaTypeFormatterTestBase<BsonMediaTypeFormatter>
    {
        // Exclude IEnumerable<T> and IQueryable<T> to avoid attempts to round trip values that are known to cause
        // trouble in deserialization e.g. base IEnumerable<T>.  BSON reader won't know how to construct such types.
        private const TestDataVariations RoundTripVariations =
            (TestDataVariations.All | TestDataVariations.WithNull | TestDataVariations.AsClassMember) &
            ~(TestDataVariations.AsIEnumerable | TestDataVariations.AsIQueryable);

        /// <summary>
        /// Provide test data for round trip tests.  Avoid types BSON does not support.
        /// <remarks>
        /// BSON does not support some unsigned integers as well as having issues with <see cref="decimal"/>.
        /// <list type="bullet">
        /// <item><description>
        /// BSON writer attempts to write an unsigned int or long as a signed integer of the same size e.g. it writes
        /// an <see cref="uint"/> as an <see cref="int"/> and thus can only write values less than
        /// <c>Int32.MaxValue</c>.  BSON writer fortunately uses an <see cref="int"/> for <see cref="sbyte"/>,
        /// <see cref="byte"/>, <see cref="short"/>, and <see cref="ushort"/> values.
        /// </description></item>
        /// <item><description>
        /// BSON successfully writes all <see cref="decimal"/> values as <see cref="double"/>.  But BSON reader may not
        /// be able to be convert the <see cref="double"/> value back e.g. <c>Decimal.MaxValue</c> loses precision when
        /// written and is rounded up -- to an invalid <see cref="decimal"/> value.
        /// </description></item>
        /// <item><description>
        /// BSON (as well as JSON and default <c>ToString()</c> in the <see cref="DateTime"/> case) loses information
        /// when writing <see cref="DateTime"/> and <see cref="DateTimeOffset"/> values.  BSON writer uses a UTC
        /// datetime value in both cases -- losing <c>Kind</c> and <c>Offset</c> property values, respectively.
        /// (<see cref="DateTime"/> values are not currently included in
        /// <see cref="CommonUnitTestDataSets.ValueAndRefTypeTestDataCollection"/> but exclude
        /// <see cref="CommonUnitTestDataSets.DateTimes"/> to be safe.)
        /// </description></item>
        /// <item><description>
        /// BSON readers and writers appear to round trip <see cref="ISerializableType"/> values successfully.  However
        /// <see cref="ISerializableType"/> does not implement <see cref="IEquatable{T}"/> or
        /// <see cref="IComparable{T}"/> and thus <see cref="Assert.Equals()"/> fails.
        /// </description></item>
        /// </list>
        /// </remarks>
        /// </summary>
        public static IEnumerable<TestData> ValueAndRefTypeTestDataCollection
        {
            get
            {
                return CommonUnitTestDataSets.ValueAndRefTypeTestDataCollection.Except(
                    new TestData[] {
                        CommonUnitTestDataSets.Uints,
                        CommonUnitTestDataSets.Ulongs,
                        CommonUnitTestDataSets.DateTimeOffsets,
                        CommonUnitTestDataSets.DateTimes,
                        CommonUnitTestDataSets.Decimals,
                        CommonUnitTestDataSets.ISerializableTypes,
                    });
            }
        }
    }
}
