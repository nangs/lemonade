﻿function Scope(a, c, f, fo, r) {
    return {
        application: {},
        resources: r ? r : [],
        configurations: c ? c : [],
        applications: a ? a : [],
        features: f ? f : [],
        featureOverrides: fo ? fo : [],
        $apply: function (func) {
            func();
        }
    }
}