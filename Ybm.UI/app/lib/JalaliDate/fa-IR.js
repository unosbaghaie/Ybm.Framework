(function (window, undefined) {
    kendo.cultures["fa-IR"] = {
        name: "fa-IR",
        numberFormat: {
            pattern: ["-n"],
            decimals: 2,
            ",": ",",
            ".": ".",
            groupSize: [3],
            percent: {
                pattern: ["-n %", "n %"],
                decimals: 2,
                ",": ",",
                ".": ".",
                groupSize: [3],
                symbol: "%"
            },
            currency: {
                pattern: ["-$n", "$n"],
                decimals: 2,
                ",": ",",
                ".": ".",
                groupSize: [3],
                symbol: "£"
            }
        },
        calendars: {
            standard: {
                days: {
                    names: ["یکشنبه", "دوشنبه", "سه شنبه", "چهارشنبه", "پنج شنبه", "جمعه", "شنبه"],
                    namesAbbr: ["یک", "دو", "سه", "چهار", "پنج", "جمعه", "شنبه"],
                    namesShort: ["ی", "د", "س", "چ", "پ", "ج", "ش"]
                },
                months: {
                    names: ["فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور", "مهر", "آبان", "آذر", "دی", "بهمن", "اسفند", ""],
                    namesAbbr: ["فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور", "مهر", "آبان", "آذر", "دی", "بهمن", "اسفند", ""]
                },
                AM: ["ق.ض", "ق.ض", "ق.ض"],
                PM: ["ب.ض", "ب.ض", "ب.ض"],
                patterns: {
                    d: "yyyy/MM/dd",
                    D: "yyyy/MM/dd",
                    F: "yyyy/MM/dd HH:mm:ss",
                    g: "yyyy/MM/dd HH:mm",
                    G: "yyyy/MM/dd HH:mm:ss",
                    m: "dd MMMM",
                    M: "dd MMMM",
                    s: "yyyy'-'MM'-'dd'T'HH':'mm':'ss",
                    t: "HH:mm",
                    T: "HH:mm:ss",
                    u: "yyyy'-'MM'-'dd HH':'mm':'ss'Z'",
                    y: "MMMM yyyy",
                    Y: "MMMM yyyy"
                },
                "/": "/",
                ":": ":",
                firstDay: 6
            }
        }
    }
})(this);


