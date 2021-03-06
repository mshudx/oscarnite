﻿// Write your Javascript code.
var createDataView = function (categoryValues, movieRatings) {
    var DataViewTransform = powerbi.data.DataViewTransform;

    var fieldExpr = powerbi.data.SQExprBuilder.fieldExpr({ column: { entity: "table1", name: "movie" } });

    var categoryIdentities = categoryValues.map(function (value) {
        var expr = powerbi.data.SQExprBuilder.equal(fieldExpr, powerbi.data.SQExprBuilder.text(value));
        return powerbi.data.createDataViewScopeIdentity(expr);
    });

    // Metadata, describes the data columns, and provides the visual with hints
    // so it can decide how to best represent the data
    var dataViewMetadata = {
        columns: [
            {
                displayName: 'Movies',
                queryName: 'movie',
                type: powerbi.ValueType.fromDescriptor({ text: true })
            },
            {
                displayName: 'Votes',
                isMeasure: true,
                format: "0",
                queryName: 'votes',
                type: powerbi.ValueType.fromDescriptor({ numeric: true }),
            }
        ]
        //,objects: { legend: { show: true, position: 'Right' } }
    };

    var columns = [
        {
            source: dataViewMetadata.columns[1],
            values: movieRatings,
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

function createDefaultStyles() {
    var dataColors = new powerbi.visuals.DataColorPalette();

    return {
        titleText: {
            color: { value: 'rgba(51,51,51,1)' }
        },
        subTitleText: {
            color: { value: 'rgba(145,145,145,1)' }
        },
        colorPalette: {
            dataColors: dataColors,
        },
        labelText: {
            color: {
                value: 'rgba(51,51,51,1)',
            },
            fontSize: '11px'
        },
        isHighContrast: false,
    };
}

function createVisual(dataViewFactory, element) {
    var pluginService = powerbi.visuals.visualPluginFactory.create();
    var defaultVisualHostServices = powerbi.visuals.defaultVisualHostServices;
    var height = 200;
    var width = 300;
    element.height(height).width(width);


    // Get a plugin
    var visual = pluginService.getPlugin('donutChart').create();

    powerbi.visuals.DefaultVisualHostServices.initialize();

    visual.init({
        // empty DOM element the visual should attach to.
        element: element,
        // host services
        host: defaultVisualHostServices,
        style: createDefaultStyles(),
        viewport: {
            height: height,
            width: width
        },
        settings: { slicingEnabled: true },
        interactivity: { isInteractiveLegend: false, selection: true },
        animation: { transitionImmediate: true }
    });

    var dataViews = [dataViewFactory()];
    var viewport = { height: height, width: width };

    if (visual.update) {
        // Call update to draw the visual with some data
        visual.update({
            dataViews: dataViews,
            viewport: viewport,
            duration: 15
        });
    } else if (visual.onDataChanged && visual.onResizing) {
        // Call onResizing and onDataChanged (old API) to draw the visual with some data
        visual.onResizing(viewport);
        visual.onDataChanged({ dataViews: dataViews });
    }
    return visual;
}
