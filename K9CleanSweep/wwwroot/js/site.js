//===============     Geolocation     ===============//
$(document).ready(function () { GetLocation(); });
var coords;
var homeCurLat = 42.9446;
var homeCurLong = -81.2108;

function SupportsGeolocation()
{
    return 'geolocation' in navigator;
}

function GetLocation()
{
    if (SupportsGeolocation())
    {
        navigator.geolocation.getCurrentPosition(ShowPosition);
    }
    else
    {
        ShowMessage("Geolocation is not supported by this browser");
    }
}

function ShowPosition(position)
{
    coords = new google.maps.LatLng(position.coords.latitude, position.coords.longitude);
    //Sets Long and Lat Of Users Position To Get Current Weather
    curLat = position.coords.latitude;
    curLong = position.coords.longitude;
    GetWeather();
}

function MyMap()
{
    var homeBaseCoords = new google.maps.LatLng(42.9446, -81.2108);
    var map = new google.maps.Map(document.getElementById("myMap"),
    {
        zoom: 8,
        center: homeBaseCoords,
        styles: [
            {
                elementType: "geometry",
                stylers: [{ color: "#242f3e" }]
            },
            {
                elementType: "labels.text.stroke",
                stylers: [{ color: "#242f3e" }]
            },
            {
                elementType: "labels.text.fill",
                stylers: [{ color: "#746855" }]
            },
            {
                featureType: "administrative.locality",
                elementType: "labels.text.fill",
                stylers: [{ color: "#D59563" }]
            },
            {
                featureType: "poi",
                elementType: "labels.text.fill",
                stylers: [{ color: "#d59563" }]
            },
            {
                featureType: "poi.park",
                elementType: "geometry",
                stylers: [{ color: "#263c3f" }]
            },
            {
                featureType: "poi.park",
                elementType: "labels.text.fill",
                stylers: [{ color: "#6b9a76" }]
            },
            {
                featureType: "road",
                elementType: "geometry",
                stylers: [{ color: "#38414e" }]
            },
            {
                featureType: "road",
                elementType: "geometry.stroke",
                stylers: [{ color: "#212a37" }]
            },
            {
                featureType: "road",
                elementType: "labels.text.fill",
                stylers: [{ color: "#9ca5b3" }]
            },
            {
                featureType: "road.highway",
                elementType: "geometry",
                stylers: [{ color: "#746855" }]
            },
            {
                featureType: "road.highway",
                elementType: "geometry.stroke",
                stylers: [{ color: "#1f2835" }]
            },
            {
                featureType: "road.highway",
                elementType: "labels.text.fill",
                stylers: [{ color: "#f3d19c" }]
            },
            {
                featureType: "transit",
                elementType: "geometry",
                stylers: [{ color: "#2f3948" }]
            },
            {
                featureType: "transit.station",
                elementType: "labels.text.fill",
                stylers: [{ color: "#d59563" }]
            },
            {
                featureType: "water",
                elementType: "geometry",
                stylers: [{ color: '#596270' }]
            },
            {
                featureType: "water",
                elementType: "labels.text.fill",
                stylers: [{ color: "#515c6d" }]
            },
            {
                featureType: "water",
                elementType: "labels.text.stroke",
                stylers: [{ color: "#17263c" }]
            }
        ]
    });

    var marker = new google.maps.Marker({
        position: homeBaseCoords,
        map: map,
        icon: "../images/mapIcon.png",
        title: "Pooch-Patrol, Main Base"
    });
    var marker = new google.maps.Marker({
        position: coords,
        map: map,
        title: "You Are Here"
    });
}

//===============     Weather Api     ===============//
var curLat = 0;
var curLong = 0;
var mainWeather;
var weatherDesc;
var currentTemp;
var city;
var country;
var weatherIcon;

function GetWeather()
{
    //Gets a Request From OWM's Weather API
    var apiRequest = new XMLHttpRequest();
    apiRequest.open("GET", "http://api.openweathermap.org/data/2.5/weather?lat=" +
        curLat + "&lon=" + curLong + "&APPID=0c9b70a5d03a96a99d87b1a0177becbe&units=metric", false);
    apiRequest.send(apiRequest.responseText);
    var weatherInfo = apiRequest.responseText;

    //Finding Main Weather
    var startPos = weatherInfo.indexOf("main", 0) + 7;
    var endPos = weatherInfo.indexOf("\"", startPos);
    mainWeather = weatherInfo.substring(startPos, endPos);

    //Finding Temperature
    var startPos = weatherInfo.indexOf("temp", 0) + 6;
    var endPos = weatherInfo.indexOf(",", startPos);
    currentTemp = weatherInfo.substring(startPos, endPos);

    //Finding Country
    var startPos = weatherInfo.indexOf("name", 0) + 7;
    var endPos = weatherInfo.indexOf("\"", startPos);
    city = weatherInfo.substring(startPos, endPos);

    //Finding City
    var startPos = weatherInfo.indexOf("country", 0) + 10;
    var endPos = weatherInfo.indexOf("\"", startPos);
    country = weatherInfo.substring(startPos, endPos);

    //Finding 
    var startPos = weatherInfo.indexOf("description", 0) + 14;
    var endPos = weatherInfo.indexOf("\"", startPos);
    weatherDesc = weatherInfo.substring(startPos, endPos);

    //Getting Weather Icon
    var startPos = weatherInfo.indexOf("icon", 0) + 7;
    var endPos = weatherInfo.indexOf("\"", startPos);
    weatherIcon = weatherInfo.substring(startPos, endPos);
    var weatherIconLink = "http://openweathermap.org/img/w/" + weatherIcon + ".png";

    //Displays The Weather in Bottom Right Corner Of Canvas / Footer
    $("#weatherIcon").attr("src", weatherIconLink);
    $("#weatherDesc").html(weatherDesc);
    $("#weatherTemp").html(currentTemp + "&deg;C");
    $("#weatherCityCountry").html(city + " , " + country);

    //FOR TESTING ALL WEATHER CANVASES (Comment all Out But One You Want To See)
    //mainWeather = "Clear";
    //mainWeather = "Clouds";
    //mainWeather = "Rain";
    //mainWeather = "Drizzle";
    //mainWeather = "Snow";
    //mainWeather = "Thunderstorm";
    DrawSky();
    MyMap();
}

var cloudTimerOn = false;
var thunderTimerOn = false;

function DrawSky()
{
    if (cloudTimerOn == true)
    {
        clearInterval(moveCloud);
    }
    if (thunderTimerOn == true)
    {
        clearInterval(moveCloud);
    }

    var backCanvas = document.getElementById("backgroundCanvas");
    backCanvas.width = window.innerWidth;
    backCanvas.height = 300;
    var ctx = backCanvas.getContext("2d");

    //If Weather Is Clear Draw Stars In Sky
    if (mainWeather == "Clear")
    {
        var xMax = backCanvas.width;
        var yMax = (backCanvas.height - 80);

        var starTimes = Math.round(xMax + yMax);

        for (var i = 0; i <= starTimes; i++)
        {
            var randomX = Math.floor((Math.random() * xMax) + 1);
            var randomY = Math.floor((Math.random() * yMax) + 1);
            var randomSize = Math.floor((Math.random() * 2) + 1);
            var randomOpacityOne = Math.floor((Math.random() * 9) + 1);
            var randomOpacityTwo = Math.floor((Math.random() * 9) + 1);
            var randomHue = Math.floor((Math.random() * 360) + 1);

            if (randomSize > 1)
            {
                ctx.shadowBlur = Math.floor((Math.random() * 15) + 5);
                ctx.shadowColor = "#FFF";
            }

            ctx.fillStyle = "hsla(" + randomHue + ", 30%, 80%, ." + randomOpacityOne +
                randomOpacityTwo + ")";
            ctx.fillRect(randomX, randomY, randomSize, randomSize);
        }
    }
    //If Weather Is Rainy Or Snowy Or Drizzly Make Background A Grayish Color
    if (mainWeather == "Snow" || mainWeather == "Rain" || mainWeather == "Drizzle")
    {
        backCanvas.style.background = "linear-gradient(#7c8587 30%, #1a1b1c)";
    }
    var xPosition = 0;
    ctx.shadowBlur = "1";
    //Calculates how many corn stalks it needs to draw
    var amountOfStalks = backCanvas.width / 20;
    var cornStalk = document.createElement("img");
    cornStalk.src = "../images/cornStalk.png";
    cornStalk.onload = function ()
    {
        for (var i = 0; i < amountOfStalks; i++)
        {
            ctx.drawImage(cornStalk, xPosition, 210, 30, 80);
            xPosition += 20;
        }
    };
    DrawFence();
}

function DrawFence()
{
    var middleCanvas = document.getElementById("middleCanvas");
    middleCanvas.width = window.innerWidth;
    middleCanvas.height = 300;
    var ctx = middleCanvas.getContext("2d");
    var fencePostAmount = middleCanvas.width / 50;
    var xPosition = 0;

    //Draws Ground
    //If Snow then Puts Snow On Ground
    if (mainWeather == "Snow")
    {
        ctx.fillStyle = "#dfe6e8";
        ctx.fillRect(0, 280, middleCanvas.width, 20);
        document.getElementById("footerContainer").style.background = "#dfe6e8";
        document.getElementById("footerContainer").style.color = "#222";
        document.getElementById("footerContainer").style.fontWeight = "bold";
        document.getElementById("footerContainer").style.fontSize = "1.2em";
    }
    //If Its Not Snow, Makes Ground Green
    else
    {
        ctx.fillStyle = "#264508";
        ctx.fillRect(0, 280, middleCanvas.width, 20);
        document.getElementById("footerContainer").style.background = "#264508";
        document.getElementById("footerContainer").style.color = "#CCC";
        document.getElementById("footerContainer").style.fontWeight = "bold";
        document.getElementById("footerContainer").style.fontSize = "1.2em";
    }

    //Draws Fence
    ctx.fillStyle = "#99430e";
    ctx.strokeStyle = "#401b04";
    ctx.fillRect(0, 240, middleCanvas.width, 10);
    ctx.fillRect(0, 260, middleCanvas.width, 10);

    //If Weather Is Snow, Fraw Snow On Top Of Fence
    if (mainWeather == "Snow")
    {
        ctx.fillStyle = "#CCC";
        ctx.strokeStyle = "";
        ctx.fillRect(0, 235, middleCanvas.width, 5);
        ctx.fillRect(0, 255, middleCanvas.width, 5);
        //Draws Snow On Top Of Each Fence Post
        for (var i = 0; i < fencePostAmount; i++)
        {
            ctx.fillStyle = "#99430e";
            ctx.strokeStyle = "#401b04";
            ctx.fillRect(xPosition, 230, 10, 50);
            ctx.fillStyle = "#CCC";
            ctx.fillRect(xPosition, 225, 10, 5);
            xPosition += 50;
        }
    }
    else
    {
        //If No Snow, Just Sraw Fence Posts
        for (var i = 0; i < fencePostAmount; i++)
        {
            ctx.fillStyle = "#99430e";
            ctx.strokeStyle = "#401b04";
            ctx.fillRect(xPosition, 230, 10, 50);
            xPosition += 50;
        }
    }
    //Chooses Which Top Canvas to Draw Dependent on Users Weather
    if (mainWeather == "Clouds")
    {
        //Draws Overcast Clouds
        DrawClouds(-100, 200, -50, 300, -800, 600);
    }
    else if (mainWeather == "Thunderstorm")
    {
        //Draws Thunderstorm
        DrawThunder(-100, 200, -50, 300, -800, 600);
    }
    else if (mainWeather == "Drizzle")
    {
        //Draws Drizzle
        DrawDrizzleRain();
    }
    else if (mainWeather == "Rain")
    {
        //Draws Rain
        DrawRain();
    }
    else if (mainWeather == "Snow")
    {
        //Draws Snow
        DrawSnow();
    }
}

function DrawClouds(x1, x2, x3, x4, x5, x6)
{
    var topCanvas = document.getElementById("topCanvas");
    topCanvas.width = window.innerWidth;
    topCanvas.height = 300;
    var ctx = topCanvas.getContext("2d");

    ctx.save();
    //Draws Static Clouds For Background
    var staticCloud1 = document.createElement("img");
    staticCloud1.src = "../images/cloud2White.png";
    staticCloud1.onload = function ()
    {
        ctx.drawImage(staticCloud1, -150, -70, 800, 200);
    };
    var staticCloud2 = document.createElement("img");
    staticCloud2.src = "../images/cloud2White.png";
    staticCloud2.onload = function ()
    {
        ctx.drawImage(staticCloud2, 800, -75, 800, 200);
    };
    var staticCloud3 = document.createElement("img");
    staticCloud3.src = "../images/cloud1Gray.png";
    staticCloud3.onload = function ()
    {
        ctx.drawImage(staticCloud3, 500, -75, 400, 200);
    };

    //Clears Canvas For Animation
    ctx.clearRect(0, 0, topCanvas.width, topCanvas.height);

    //Moving Clouds
    var cloud1 = document.createElement("img");
    cloud1.src = "../images/cloud1Gray.png";
    cloud1.onload = function ()
    {
        ctx.drawImage(cloud1, x1, -50, 800, 200);
    };
    var cloud2 = document.createElement("img");
    cloud2.src = "../images/cloud1Gray.png";
    cloud2.onload = function ()
    {
        ctx.drawImage(cloud2, x2, -50, 600, 120);
    };
    var cloud3 = document.createElement("img");
    cloud3.src = "../images/cloud2Dark.png";
    cloud3.onload = function ()
    {
        ctx.drawImage(cloud3, x3, -50, 600, 150);
    };
    var cloud4 = document.createElement("img");
    cloud4.src = "../images/cloud1Gray.png";
    cloud4.onload = function ()
    {
        ctx.drawImage(cloud4, x4, -50, 600, 200);
    };
    var cloud5 = document.createElement("img");
    cloud5.src = "../images/cloud1Dark.png";
    cloud5.onload = function ()
    {
        ctx.drawImage(cloud5, x5, -50, 700, 200);
    };
    var cloud6 = document.createElement("img");
    cloud6.src = "../images/cloud1Dark.png";
    cloud6.onload = function ()
    {
        ctx.drawImage(cloud6, x6, -80, 600, 250);
    };
    ctx.restore();

    //Adjusts position of X-Axis of each cloud differently for different speeds
    x1 += 0.7;
    x2 += 0.4;
    x3 += 0.9;
    x4 += 0.8;
    x5 += 0.4;
    x6 += 0.3;

    //Resets Clouds If Fall Off Canvas
    if (x1 > (topCanvas.width + 50))
    {
        x1 = -600;
    }
    if (x2 > (topCanvas.width + 50))
    {
        x2 = -600;
    }
    if (x3 > (topCanvas.width + 50))
    {
        x3 = -600;
    }
    if (x4 > (topCanvas.width + 50))
    {
        x4 = -600;
    }
    if (x5 > (topCanvas.width + 50))
    {
        x5 = -600;
    }
    if (x6 > (topCanvas.width + 50))
    {
        x6 = -600;
    }

    //Delay For Redraw Of Canvas (Good Place To Set Speed Of FrameRate)
    var moveCloud = setTimeout("DrawClouds(" + x1 + "," + x2 + "," + x3 + "," + x4 +
        "," + x5 + "," + x6 + ")", 100);
    //CloudTimerOn Is For Clearing The Interval Timers If You Resize Window
    cloudTimerOn = true;
}

//Used for thunder Timer
var thunderCounter = 0;

function DrawThunder(x1, x2, x3, x4, x5, x6) // Parameters are Each Clouds X-Axis Value
{
    var topCanvas = document.getElementById("topCanvas");
    topCanvas.width = window.innerWidth;
    topCanvas.height = 300;
    var ctx = topCanvas.getContext("2d");

    ctx.save();
    //Draws Static Clouds For Background
    var staticCloud1 = document.createElement("img");
    staticCloud1.src = "../images/cloud2Darkest.png";
    staticCloud1.onload = function ()
    {
        ctx.drawImage(staticCloud1, -150, -70, 800, 200);
    };
    var staticCloud2 = document.createElement("img");
    staticCloud2.src = "../images/cloud2Dark.png";
    staticCloud2.onload = function ()
    {
        ctx.drawImage(staticCloud2, 800, -75, 800, 200);
    };
    var staticCloud3 = document.createElement("img");
    staticCloud3.src = "../images/cloud1Dark.png";
    staticCloud3.onload = function ()
    {
        ctx.drawImage(staticCloud3, 500, -75, 400, 200);
    };
    var staticCloud4 = document.createElement("img");
    staticCloud4.src = "../images/cloud2Darkest.png";
    staticCloud4.onload = function ()
    {
        ctx.drawImage(staticCloud4, -150, -70, 800, 200);
    };

    //Clears Canvas for Animation
    ctx.clearRect(0, 0, topCanvas.width, topCanvas.height);

    //Moving Clouds
    var cloud1 = document.createElement("img");
    cloud1.src = "../images/cloud1Dark.png";
    cloud1.onload = function ()
    {
        ctx.drawImage(cloud1, x1, -50, 800, 200);
    };
    var cloud2 = document.createElement("img");
    cloud2.src = "../images/cloud1Darkest.png";
    cloud2.onload = function ()
    {
        ctx.drawImage(cloud2, x2, -50, 600, 120);
    };
    var cloud3 = document.createElement("img");
    cloud3.src = "../images/cloud2Dark.png";
    cloud3.onload = function ()
    {
        ctx.drawImage(cloud3, x3, -50, 600, 150);
    };
    var cloud4 = document.createElement("img");
    cloud4.src = "../images/cloud1Dark.png";
    cloud4.onload = function ()
    {
        ctx.drawImage(cloud4, x4, -50, 600, 200);
    };
    var cloud5 = document.createElement("img");
    cloud5.src = "../images/cloud1Dark.png";
    cloud5.onload = function ()
    {
        ctx.drawImage(cloud5, x5, -50, 700, 200);
    };
    var cloud6 = document.createElement("img");
    cloud6.src = "../images/cloud1Dark.png";
    cloud6.onload = function ()
    {
        ctx.drawImage(cloud6, x6, -80, 600, 250);
    };
    ctx.restore();
    //Adjusts position of X-Axis of each cloud differently for different speeds
    x1 += 0.7;
    x2 += 0.4;
    x3 += 0.9;
    x4 += 0.8;
    x5 += 0.4;
    x6 += 0.3;

    //Resets Clouds If Fall Off Canvas
    if (x1 > (topCanvas.width + 50))
    {
        x1 = -600;
    }
    if (x2 > (topCanvas.width + 50))
    {
        x2 = -600;
    }
    if (x3 > (topCanvas.width + 50))
    {
        x3 = -600;
    }
    if (x4 > (topCanvas.width + 50))
    {
        x4 = -600;
    }
    if (x5 > (topCanvas.width + 50))
    {
        x5 = -600;
    }
    if (x6 > (topCanvas.width + 50))
    {
        x6 = -600;
    }

    if (thunderCounter == 300)
    {
        //Chooses A Cloud at Random To Flash
        var randNum = Math.floor((Math.random() * 6) + 1);
        switch (randNum)
        {
            case 1:
                {
                    thunderCloud(cloud1);
                    break;
                }
            case 2:
                {
                    thunderCloud(cloud2);
                    break;
                }
            case 3:
                {
                    thunderCloud(cloud3);
                    break;
                }
            case 4:
                {
                    thunderCloud(cloud4);
                    break;
                }
            case 5:
                {
                    thunderCloud(cloud5);
                    break;
                }
            case 6:
                {
                    thunderCloud(cloud6);
                    break;
                }
        }
    }
    thunderCounter++;
    //Delay For Redraw Of Canvas (Good Place To Set Speed Of FrameRate)
    var moveCloud = setTimeout("DrawThunder(" + x1 + "," + x2 + "," + x3 + "," + x4 +
        "," + x5 + "," + x6 + ")", 100);
    //CloudTimerOn Is For Clearing The Interval Timers If You Resize Window
    cloudTimerOn = true;
}
function thunderCloud(cloud)
{
    //Flashes Cloud To White / Dark 5 Times
    var topCanvas = document.getElementById("topCanvas");
    var ctx = topCanvas.getContext("2d");

    cloud.src = "../images/cloud1White.png";
    setTimeout(function ()
    {
        cloud.src = "../images/cloud1Darkest.png";
    }, 100);
    setTimeout(function ()
    {
        cloud.src = "../images/cloud1White.png";
    }, 300);
    setTimeout(function ()
    {
        cloud.src = "../images/cloud1Darkest.png";
    }, 500);
    setTimeout(function ()
    {
        cloud.src = "../images/cloud1White.png";
    }, 800);
    setTimeout(function ()
    {
        cloud.src = "../images/cloud1Darkest.png";
    }, 900);
    thunderCounter = 0;
}

function DrawSnow()
{
    var topCanvas = document.getElementById("topCanvas");
    topCanvas.width = window.innerWidth;
    topCanvas.height = 300;
    var ctx = topCanvas.getContext("2d");

    var width = topCanvas.width;
    var height = topCanvas.height;

    var maxSnowflakes = 100;
    var snowflakes = [];

    for (var i = 0; i < maxSnowflakes; i++)
    {
        //Replaces An Old Flake With New Flake
        snowflakes.push
        ({
            x: Math.random() * width,           //x-coordinate
            y: Math.random() * height,          //y-coordinate
            r: Math.random() * 4 + 1,           //radius
            d: Math.random() * maxSnowflakes    //density
        })
    }
    //Draws Snowflakes
    function DrawSnowflakes()
    {
        //Clears Canvas For Animation
        ctx.clearRect(0, 0, width, height);

        ctx.fillStyle = "rgba(255, 255, 255, 0.8)";
        ctx.beginPath();

        for (var i = 0; i < maxSnowflakes; i++)
        {
            var flake = snowflakes[i];
            ctx.moveTo(flake.x, flake.y);
            ctx.arc(flake.x, flake.y, flake.r, 0, Math.PI * 2, true);
        }
        ctx.fill();
    UpdateSnowflakes();
    }

    var angle = 0;

    function UpdateSnowflakes()
    {
        angle += 0.01;

        for (var i = 0; i < maxSnowflakes; i++)
        {
            var flake = snowflakes[i];

            //Falls At Random Speed
            flake.y += Math.floor((Math.random() * 3) + 1);

            //Sends Snow Back To Top When Exits Canvas
            if (flake.x > width || flake.x < 0 || flake.y > (height - 20))
            {
                snowflakes[i] =
                    {
                    x: Math.random() * width, y: -10, r: flake.r, d: flake.d
                    }
            }
        }
    }
    setInterval(DrawSnowflakes, 30);
    DrawSnowClouds(-100, 200, -50, 300, -800, 600);
}

function DrawSnowClouds(x1, x2, x3, x4, x5, x6) // Parameters are Each Clouds X-Axis Value
{
    var veryTopCanvas = document.getElementById("veryTopCanvas");
    veryTopCanvas.width = window.innerWidth;
    veryTopCanvas.height = 300;
    var ctx = veryTopCanvas.getContext("2d");

    ctx.save();
    //Draws Static Clouds For Background
    var staticCloud1 = document.createElement("img");
    staticCloud1.src = "../images/cloud2Darkest.png";
    staticCloud1.onload = function ()
    {
        ctx.drawImage(staticCloud1, -150, -70, 800, 200);
    };
    var staticCloud2 = document.createElement("img");
    staticCloud2.src = "../images/cloud2Dark.png";
    staticCloud2.onload = function ()
    {
        ctx.drawImage(staticCloud2, 800, -75, 800, 200);
    };
    var staticCloud3 = document.createElement("img");
    staticCloud3.src = "../images/cloud1Dark.png";
    staticCloud3.onload = function ()
    {
        ctx.drawImage(staticCloud3, 500, -75, 400, 200);
    };
    var staticCloud4 = document.createElement("img");
    staticCloud4.src = "../images/cloud2Darkest.png";
    staticCloud4.onload = function ()
    {
        ctx.drawImage(staticCloud4, -150, -70, 800, 200);
    };

    //Clears Canvas for Animation
    ctx.clearRect(0, 0, topCanvas.width, topCanvas.height);

    //Moving Clouds
    var cloud1 = document.createElement("img");
    cloud1.src = "../images/cloud1Dark.png";
    cloud1.onload = function ()
    {
        ctx.drawImage(cloud1, x1, -50, 800, 200);
    };
    var cloud2 = document.createElement("img");
    cloud2.src = "../images/cloud1Darkest.png";
    cloud2.onload = function ()
    {
        ctx.drawImage(cloud2, x2, -50, 600, 120);
    };
    var cloud3 = document.createElement("img");
    cloud3.src = "../images/cloud2Dark.png";
    cloud3.onload = function ()
    {
        ctx.drawImage(cloud3, x3, -50, 600, 150);
    };
    var cloud4 = document.createElement("img");
    cloud4.src = "../images/cloud1Dark.png";
    cloud4.onload = function ()
    {
        ctx.drawImage(cloud4, x4, -50, 600, 200);
    };
    var cloud5 = document.createElement("img");
    cloud5.src = "../images/cloud1Dark.png";
    cloud5.onload = function ()
    {
        ctx.drawImage(cloud5, x5, -50, 700, 200);
    };
    var cloud6 = document.createElement("img");
    cloud6.src = "../images/cloud1Dark.png";
    cloud6.onload = function ()
    {
        ctx.drawImage(cloud6, x6, -80, 600, 250);
    };
    ctx.restore();
    //Adjusts position of X-Axis of each cloud differently for different speeds
    x1 += 0.7;
    x2 += 0.4;
    x3 += 0.9;
    x4 += 0.8;
    x5 += 0.4;
    x6 += 0.3;

    //Resets Clouds If Fall Off Canvas
    if (x1 > (topCanvas.width + 50))
    {
        x1 = -600;
    }
    if (x2 > (topCanvas.width + 50))
    {
        x2 = -600;
    }
    if (x3 > (topCanvas.width + 50))
    {
        x3 = -600;
    }
    if (x4 > (topCanvas.width + 50))
    {
        x4 = -600;
    }
    if (x5 > (topCanvas.width + 50))
    {
        x5 = -600;
    }
    if (x6 > (topCanvas.width + 50))
    {
        x6 = -600;
    }

    //Delay For Redraw Of Canvas (Good Place To Set Speed Of FrameRate)
    var moveCloud = setTimeout("DrawSnowClouds(" + x1 + "," + x2 + "," + x3 + "," + x4 +
        "," + x5 + "," + x6 + ")", 100);
    //CloudTimerOn Is For Clearing The Interval Timers If You Resize Window
    cloudTimerOn = true;
}

function DrawRain()
{
    var topCanvas = document.getElementById("topCanvas");
    topCanvas.width = window.innerWidth;
    topCanvas.height = 300;
    var ctx = topCanvas.getContext("2d");

    var width = topCanvas.width;
    var height = topCanvas.height;

    var maxRainDrops = 1000;
    var rainDrops = [];

    for (var i = 0; i < maxRainDrops; i++)
    {
        //Replaces An Old Drop With New Drop
        rainDrops.push
            ({
                x: Math.random() * width,           //x-coordinate
                y: Math.random() * height,          //y-coordinate
                r: Math.random() * 4 + 1,           //radius
                d: Math.random() * maxRainDrops    //density
            })
    }
    //Draws Rain
    function DrawRainDrops()
    {
        //Clears Canvas For Animation
        ctx.clearRect(0, 0, width, height);

        ctx.fillStyle = "rgba(134, 154, 158, 0.8)";
        ctx.beginPath();

        for (var i = 0; i < maxRainDrops; i++)
        {
            var drop = rainDrops[i];
            ctx.moveTo(drop.x, drop.y);
            ctx.fillRect(drop.x, drop.y, 2, 6);
        }
        ctx.fill();
        UpdateRainDrops();
    }

    var angle = 0;

    function UpdateRainDrops()
    {
        angle += 0.01;

        for (var i = 0; i < maxRainDrops; i++)
        {
            var drop = rainDrops[i];

            //Falls At Random Speed
            drop.y += 8;
            //drop.x += -2;

            //Sends Rain Back To Top When Exits Canvas
            if (drop.x > width || drop.x < 0 || drop.y > (height - 20)) {
                rainDrops[i] =
                    {
                    x: Math.random() * width, y: -10, r: drop.r, d: drop.d
                    }
            }
        }
    }
    setInterval(DrawRainDrops, 30);
    DrawRainClouds(-100, 200, -50, 300, -800, 600);
}

function DrawRainClouds(x1, x2, x3, x4, x5, x6)
{
    var veryTopCanvas = document.getElementById("veryTopCanvas");
    veryTopCanvas.width = window.innerWidth;
    veryTopCanvas.height = 300;
    var ctx = veryTopCanvas.getContext("2d");

    ctx.save();
    //Draws Static Clouds For Background
    var staticCloud1 = document.createElement("img");
    staticCloud1.src = "../images/cloud2White.png";
    staticCloud1.onload = function ()
    {
        ctx.drawImage(staticCloud1, -150, -70, 800, 200);
    };
    var staticCloud2 = document.createElement("img");
    staticCloud2.src = "../images/cloud2White.png";
    staticCloud2.onload = function ()
    {
        ctx.drawImage(staticCloud2, 800, -75, 800, 200);
    };
    var staticCloud3 = document.createElement("img");
    staticCloud3.src = "../images/cloud1Gray.png";
    staticCloud3.onload = function ()
    {
        ctx.drawImage(staticCloud3, 500, -75, 400, 200);
    };

    //Clears Canvas For Animation
    ctx.clearRect(0, 0, topCanvas.width, topCanvas.height);

    //Moving Clouds
    var cloud1 = document.createElement("img");
    cloud1.src = "../images/cloud1Gray.png";
    cloud1.onload = function ()
    {
        ctx.drawImage(cloud1, x1, -50, 800, 200);
    };
    var cloud2 = document.createElement("img");
    cloud2.src = "../images/cloud1Gray.png";
    cloud2.onload = function ()
    {
        ctx.drawImage(cloud2, x2, -50, 600, 120);
    };
    var cloud3 = document.createElement("img");
    cloud3.src = "../images/cloud2Dark.png";
    cloud3.onload = function ()
    {
        ctx.drawImage(cloud3, x3, -50, 600, 150);
    };
    var cloud4 = document.createElement("img");
    cloud4.src = "../images/cloud1Gray.png";
    cloud4.onload = function ()
    {
        ctx.drawImage(cloud4, x4, -50, 600, 200);
    };
    var cloud5 = document.createElement("img");
    cloud5.src = "../images/cloud1Dark.png";
    cloud5.onload = function ()
    {
        ctx.drawImage(cloud5, x5, -50, 700, 200);
    };
    var cloud6 = document.createElement("img");
    cloud6.src = "../images/cloud1Dark.png";
    cloud6.onload = function ()
    {
        ctx.drawImage(cloud6, x6, -80, 600, 250);
    };
    ctx.restore();

    //Adjusts position of X-Axis of each cloud differently for different speeds
    x1 += 0.7;
    x2 += 0.4;
    x3 += 0.9;
    x4 += 0.8;
    x5 += 0.4;
    x6 += 0.3;

    //Resets Clouds If Fall Off Canvas
    if (x1 > (topCanvas.width + 50))
    {
        x1 = -600;
    }
    if (x2 > (topCanvas.width + 50))
    {
        x2 = -600;
    }
    if (x3 > (topCanvas.width + 50))
    {
        x3 = -600;
    }
    if (x4 > (topCanvas.width + 50))
    {
        x4 = -600;
    }
    if (x5 > (topCanvas.width + 50))
    {
        x5 = -600;
    }
    if (x6 > (topCanvas.width + 50))
    {
        x6 = -600;
    }

    //Delay For Redraw Of Canvas
    var moveCloud = setTimeout("DrawRainClouds(" + x1 + "," + x2 + "," + x3 + "," + x4 +
        "," + x5 + "," + x6 + ")", 100);
}

function DrawDrizzleRain()
{
    var topCanvas = document.getElementById("topCanvas");
    topCanvas.width = window.innerWidth;
    topCanvas.height = 300;
    var ctx = topCanvas.getContext("2d");

    var width = topCanvas.width;
    var height = topCanvas.height;

    var maxRainDrops = 500;
    var rainDrops = [];

    for (var i = 0; i < maxRainDrops; i++)
    {
        //Replaces An Old Drop With New Drop
        rainDrops.push
            ({
                x: Math.random() * width,           //x-coordinate
                y: Math.random() * height,          //y-coordinate
                r: Math.random() * 1,           //radius
                d: Math.random() * maxRainDrops    //density
            })
    }
    //Draws Rain
    function DrawRainDrops() {
        //Clears Canvas For Animation
        ctx.clearRect(0, 0, width, height);

        ctx.fillStyle = "rgba(134, 154, 158, 0.8)";
        ctx.beginPath();

        for (var i = 0; i < maxRainDrops; i++)
        {
            var drop = rainDrops[i];
            ctx.moveTo(drop.x, drop.y);
            ctx.arc(drop.x, drop.y, drop.r, 0, Math.PI * 2, true);
        }
        ctx.fill();
        UpdateRainDrops();
    }

    function UpdateRainDrops()
    {

        for (var i = 0; i < maxRainDrops; i++)
        {
            var drop = rainDrops[i];

            drop.y += 0.5;

            //Sends Rain Back To Top When Exits Canvas
            if (drop.x > width || drop.x < 0 || drop.y > (height - 20)) {
                rainDrops[i] =
                    {
                        x: Math.random() * width, y: -10, r: drop.r, d: drop.d
                    }
            }
        }
    }
    setInterval(DrawRainDrops, 30);
    DrawDrizzleClouds(-100, 200, -50, 300, -800, 600);
}

function DrawDrizzleClouds(x1, x2, x3, x4, x5, x6)
{
    var veryTopCanvas = document.getElementById("veryTopCanvas");
    veryTopCanvas.width = window.innerWidth;
    veryTopCanvas.height = 300;
    var ctx = veryTopCanvas.getContext("2d");

    ctx.save();
    //Draws Static Clouds For Background
    var staticCloud1 = document.createElement("img");
    staticCloud1.src = "../images/cloud2White.png";
    staticCloud1.onload = function ()
    {
        ctx.drawImage(staticCloud1, -150, -70, 800, 200);
    };
    var staticCloud2 = document.createElement("img");
    staticCloud2.src = "../images/cloud2White.png";
    staticCloud2.onload = function ()
    {
        ctx.drawImage(staticCloud2, 800, -75, 800, 200);
    };
    var staticCloud3 = document.createElement("img");
    staticCloud3.src = "../images/cloud1Gray.png";
    staticCloud3.onload = function ()
    {
        ctx.drawImage(staticCloud3, 500, -75, 400, 200);
    };

    //Clears Canvas For Animation
    ctx.clearRect(0, 0, topCanvas.width, topCanvas.height);

    //Moving Clouds
    var cloud1 = document.createElement("img");
    cloud1.src = "../images/cloud1Gray.png";
    cloud1.onload = function ()
    {
        ctx.drawImage(cloud1, x1, 50, 800, 200);
    };
    var cloud2 = document.createElement("img");
    cloud2.src = "../images/cloud1Gray.png";
    cloud2.onload = function ()
    {
        ctx.drawImage(cloud2, x2, 50, 800, 120);
    };
    var cloud3 = document.createElement("img");
    cloud3.src = "../images/cloud2Dark.png";
    cloud3.onload = function ()
    {
        ctx.drawImage(cloud3, x3, 100, 1200, 150);
    };
    var cloud4 = document.createElement("img");
    cloud4.src = "../images/cloud1Gray.png";
    cloud4.onload = function ()
    {
        ctx.drawImage(cloud4, x4, 100, 1100, 200);
    };
    var cloud5 = document.createElement("img");
    cloud5.src = "../images/cloud1Dark.png";
    cloud5.onload = function ()
    {
        ctx.drawImage(cloud5, x5, 100, 1200, 200);
    };
    var cloud6 = document.createElement("img");
    cloud6.src = "../images/cloud1Dark.png";
    cloud6.onload = function ()
    {
        ctx.drawImage(cloud6, x6, 100, 1200, 250);
    };
    ctx.restore();

    //Adjusts position of X-Axis of each cloud differently for different speeds
    x1 += 0.7;
    x2 += 0.4;
    x3 += 0.9;
    x4 += 0.8;
    x5 += 0.4;
    x6 += 0.3;

    //Resets Clouds If Fall Off Canvas
    if (x1 > (topCanvas.width + 50))
    {
        x1 = -600;
    }
    if (x2 > (topCanvas.width + 50))
    {
        x2 = -600;
    }
    if (x3 > (topCanvas.width + 50))
    {
        x3 = -600;
    }
    if (x4 > (topCanvas.width + 50))
    {
        x4 = -600;
    }
    if (x5 > (topCanvas.width + 50))
    {
        x5 = -600;
    }
    if (x6 > (topCanvas.width + 50))
    {
        x6 = -600;
    }

    //Delay For Redraw Of Canvas (Good Place To Set Speed Of FrameRate)
    var moveCloud = setTimeout("DrawDrizzleClouds(" + x1 + "," + x2 + "," + x3 + "," + x4 +
        "," + x5 + "," + x6 + ")", 100);
    //CloudTimerOn Is For Clearing The Interval Timers If You Resize Window
    cloudTimerOn = true;
}

//Redraws Canvas on Resize
window.onresize = function (event)
{
    DrawSky();
};

function CheckStarChoice()
{
    var starRadios = document.getElementsByName("stars");
    for (var i = 0; i < starRadios.length; i++)
    {
        if (starRadios[i].checked) {
            var starValue = starRadios[i].value;
            break;
        }
    }
    switch (starValue)
    {
        case "1":
            {
                document.getElementById("oneStar").style.background = "#fcee21";
                document.getElementById("twoStar").style.background = "#423615";
                document.getElementById("threeStar").style.background = "#423615";
                document.getElementById("fourStar").style.background = "#423615";
                document.getElementById("fiveStar").style.background = "#423615";
                break;
            }
        case "2":
            {
                document.getElementById("oneStar").style.background = "#fcee21";
                document.getElementById("twoStar").style.background = "#fcee21";
                document.getElementById("threeStar").style.background = "#423615";
                document.getElementById("fourStar").style.background = "#423615";
                document.getElementById("fiveStar").style.background = "#423615";
                break;
            }
        case "3":
            {
                document.getElementById("oneStar").style.background = "#fcee21";
                document.getElementById("twoStar").style.background = "#fcee21";
                document.getElementById("threeStar").style.background = "#fcee21";
                document.getElementById("fourStar").style.background = "#423615";
                document.getElementById("fiveStar").style.background = "#423615";
                break;
            }
        case "4":
            {
                document.getElementById("oneStar").style.background = "#fcee21";
                document.getElementById("twoStar").style.background = "#fcee21";
                document.getElementById("threeStar").style.background = "#fcee21";
                document.getElementById("fourStar").style.background = "#fcee21";
                document.getElementById("fiveStar").style.background = "#423615";
                break;
            }
        case "5":
            {
                document.getElementById("oneStar").style.background = "#fcee21";
                document.getElementById("twoStar").style.background = "#fcee21";
                document.getElementById("threeStar").style.background = "#fcee21";
                document.getElementById("fourStar").style.background = "#fcee21";
                document.getElementById("fiveStar").style.background = "#fcee21";
                break;
            }
    }
}

//===============     My Canvas Clock     ===============//
function DegToRad(degree)
{
    var factor = Math.PI / 180;
    return degree * factor;
}

function UpdateCanvasClock()
{
    var canvas = document.getElementById("indexCanvas");
    var ctx = canvas.getContext("2d");
    ctx.strokeStyle = "#2c2d30";
    ctx.lineWidth = 10;
    ctx.lineCap = "round";
    ctx.shadowBlur = 10;
    ctx.shadowColor = "#2c2d30";

    var now = new Date();
    var today = now.toDateString();
    var time = now.toLocaleTimeString();
    var hours = now.getHours();
    var minutes = now.getMinutes();
    var seconds = now.getSeconds();
    var milliseconds = now.getMilliseconds();
    var newSeconds = seconds + (milliseconds / 1000);

    //Background
    gradient = ctx.createRadialGradient(100, 100, 5, 100, 100, 200);
    gradient.addColorStop(0, "#CCC");
    gradient.addColorStop(1, "#98a6bc");
    ctx.fillStyle = gradient;
    ctx.fillRect(0, 0, 200, 200);

    //Draws Hours
    ctx.beginPath();
    ctx.arc(100, 100, 80, DegToRad(270), DegToRad((hours * 15) - 90));
    ctx.stroke();

    //Draws Minutes
    ctx.beginPath();
    ctx.arc(100, 100, 60, DegToRad(270), DegToRad((minutes * 6) - 90));
    ctx.stroke();

    //Seconds
    ctx.beginPath();
    ctx.arc(100, 100, 40, DegToRad(270), DegToRad((newSeconds * 6) - 90));
    ctx.stroke();

    //Time
    ctx.font = "10px Ariel bold";
    ctx.fillStyle = "#2c2d30";
    ctx.fillText(time, 75, 100);
}
setInterval(UpdateCanvasClock, 40);