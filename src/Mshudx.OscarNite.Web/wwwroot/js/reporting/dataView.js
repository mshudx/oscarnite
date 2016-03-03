/// <reference path="../../lib/powerbi-visuals.all.min.js" />

var createDataView = function () {
    var DataViewTransform = powerbi.data.DataViewTransform;

    var fieldExpr = powerbi.data.SQExprBuilder.fieldExpr({ column: { entity: "table1", name: "country" } });

    var categoryValues = ["Australia", "Canada", "France", "Germany", "United Kingdom", "United States"];
    var categoryIdentities = categoryValues.map(function (value) {
        var expr = powerbi.data.SQExprBuilder.equal(fieldExpr, powerbi.data.SQExprBuilder.text(value));
        return powerbi.data.createDataViewScopeIdentity(expr);
    });

    // Metadata, describes the data columns, and provides the visual with hints
    // so it can decide how to best represent the data
    var dataViewMetadata = {
        columns: [
            {
                displayName: 'Country',
                queryName: 'Country',
                type: powerbi.ValueType.fromDescriptor({ text: true })
            },
            {
                displayName: 'Sales Amount (2014)',
                isMeasure: true,
                format: "$0",
                queryName: 'sales1',
                type: powerbi.ValueType.fromDescriptor({ numeric: true }),
            },
            {
                displayName: 'Sales Amount (2013)',
                isMeasure: true,
                format: "$0",
                queryName: 'sales2',
                type: powerbi.ValueType.fromDescriptor({ numeric: true })
            }
        ],
    };

    var columns = [
        {
            source: dataViewMetadata.columns[1],
            // Sales Amount for 2014
            values: [742731.43, 162066.43, 283085.78, 300263.49, 376074.57, 814724.34],
        },
        {
            source: dataViewMetadata.columns[2],
            // Sales Amount for 2013
            values: [742731.43, 162066.43, 283085.78, 300263.49, 376074.57, 814724.34].reverse()
        }
    ];

    var dataValues = DataViewTransform.createValueColumns(columns);

    var dataView = {
        metadata: dataViewMetadata,
        categorical: {
            categories: [{
                source: dataViewMetadata.columns[0],
                values: categoryValues,
                identity: categoryIdentities,
            }],
            values: dataValues
        }
    };

    return dataView;
};