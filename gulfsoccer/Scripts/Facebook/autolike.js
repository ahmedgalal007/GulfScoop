//<script type="txt/javascript">

$(".fb-like iframe").on("load",
    setTimeout(
    function () {
    $(this).contents().find('table button[type="submit"]').trigger("click").text('Fb');
},2000));
//</script>
/*
<div id="fb-root"></div>
    <script async defer crossorigin="anonymous" src="https://connect.facebook.net/ar_AR/sdk.js#xfbml=1&version=v5.0&appId=1931095040241660&autoLogAppEvents=1"></script>

<div class="fb-like" data-href="https://www.facebook.com/Ahly.com.eg/" data-width="" data-layout="box_count" data-action="like" data-size="large" data-share="false"></div>

*/