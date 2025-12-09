<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="FAQ.aspx.cs"
    Inherits="ThermoWeb.FAQ" EnableEventValidation="false" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Fichas de parametros</title>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
    <%-- <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.4.1/css/bootstrap.min.css" integrity="sha384-Vkoo8x4CGsO3+Hhxv8T/Q5PaXtkKtu6ug5TOeNV6gBiFeWPGFN9MuhOf23Q9Ifjh" crossorigin="anonymous"> --%>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.3/umd/popper.min.js" integrity="sha384-ZMP7rVo3mIykV+2+9J3UJ46jBk0WLaUAdn689aCwoqbBJiSnjAK/l8WvCWPIPm49" crossorigin="anonymous"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
    <%-- <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.4.1/js/bootstrap.min.js" integrity="sha384-wfSDF2E50Y2D1uUdj0O3uMBJnjuUD4Ih7YwaYd1iqfktj0Uod8GCExl3Og8ifwB6" crossorigin="anonymous"></script> --%>

    <script src="js/json2.js" type="text/javascript"></script>
</head>
<body>
    <form id="cabecera1" runat="server">
    <nav class="navbar navbar-inverse">
  <div class="container-fluid">
    <div class="navbar-header">
      <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#myNavbar">
        <span class="icon-bar"></span>
        <span class="icon-bar"></span>
        <span class="icon-bar"></span>                        
      </button>
      <a class="navbar-brand" href="index.aspx">Thermolympic S.L.</a>
    </div>
    <div class="collapse navbar-collapse" id="myNavbar">
      <ul class="nav navbar-nav"> 
        <li><a href="FichasParametros_nuevo.aspx">Crear ficha</a></li>
        <li><a href="FichasParametros.aspx">Consultar ficha de fabricación</a></li>      
      </ul>
      <ul class="nav navbar-nav navbar-right">
      <li><a href="#"><span class="glyphicon glyphicon-question-sign"> AYUDA</span></a></li>
    </ul>
    </div>
  </div>
</nav>
<!-- Bootstrap FAQ - START -->
<div class="container">
    <br />
    <br />
    <div class="alert alert-warning alert-dismissible" role="alert">
        <button type="button" class="close" data-dismiss="alert"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
        En esta sección encontrarás información sobre el uso de la <strong>aplicación de parámetros</strong>. Para información de otras aplicaciones, por favor consulta las secciones F.A.Q. en sus respectivas aplicaciones. 
    </div>
    <div class="panel-group" id="accordion">
        <div class="faqHeader">Cuestiones generales</div>
        <div class="panel panel-default">
            <div class="panel-heading">
                <h4 class="panel-title">
                    <a class="accordion-toggle" data-toggle="collapse" data-parent="#accordion" href="#collapseOne">¿Qué es la aplicación de parámetros?</a>
                </h4>
            </div>
            <div id="collapseOne" class="panel-collapse collapse in">
                <div class="panel-body">
                    La aplicación de parámetros de <em>Thermolympic S.L.</em> es un programa online en el que podrás crear, consultar, modificar y descargar las <strong>Fichas de 
                    Parámetros</strong> de las piezas que fabricamos en la empresa.</div>
            </div>
        </div>
        <div class="panel panel-default">
            <div class="panel-heading">
                <h4 class="panel-title">
                    <a class="accordion-toggle collapsed" data-toggle="collapse" data-parent="#accordion" href="#collapseTen">¿Qué necesito para usarla?</a>
                </h4>
            </div>
            <div id="collapseTen" class="panel-collapse collapse">
                <div class="panel-body">
                    La aplicación funciona desde el servidor local de Thermolympic S.L., para usarla 
                    necesitarás:<ul>
                        <li>Un dispositivo con un navegador compatible (Chrome, Firefox, Opera...)</li>
                        <li>Estar conectado a la red de empresa &quot;Thermolympic Corporativa&quot;</li>
                        <li>Acceder a la aplicación a en <a href="http://facts4-srv/thermogestion/">
                            http://facts4-srv/thermogestion/</a> y entrar la sección<a 
                                href="http://facts4-srv/thermogestion/FichasParametros.aspx"> Parámetros</a></li>
                    </ul>
                </div>
            </div>
        </div>
        <div class="faqHeader">Navegación</div>
        <div class="panel panel-default">
            <div class="panel-heading">
                <h4 class="panel-title">
                    <a class="accordion-toggle collapsed" data-toggle="collapse" data-parent="#accordion" href="#collapseTwo">Secciones de la aplicación</a>
                </h4>
            </div>
            <div id="collapseTwo" class="panel-collapse collapse">
                <div class="panel-body">
                    La aplicación cuenta con un menú en la cabecera desde donde podremos acceder a 
                    las siguientes secciones:<ul>
                        <li><strong>Crear Ficha:</strong> Desde esta opción accederemos a un formato en 
                            blanco en el cual podremos crear una <strong>ficha de parámetros</strong> nueva.</li>
                        <li><strong>Consultar ficha:</strong> Desde esta opción accederemos al formato de 
                            consulta. Desde aquí podremos:<ul>
                                <li>Cargar una ficha de parámetros existente para consultarla.</li>
                                <li>Modificar una ficha de parámetros existente.</li>
                                <li>Consultar la estrucura del producto cargado (sincronizado con el ERP).</li>
                                <li>Descargar en excel la ficha de parámetros cargada. </li>
                            </ul>
                        </li>
                    </ul>
                    <ul>
                        <li><strong>Ver listado de fichas:</strong> Desde esta accederemos al listado de 
                            fichas existentes en la base de datos. Desde aquí podremos:<ul>
                                <li>Ver el listado y acceder las fichas de fabricación.</li>
                                <li>Eliminar la ficha de fabricación seleccionada (este paso borrará todas las 
                                    versiones de la ficha).</li>
                            </ul>
                        </li>
                    </ul>
                </div>
            </div>
        </div>
        <div class="panel panel-default">
            <div class="panel-heading">
                <h4 class="panel-title">
                    <a class="accordion-toggle collapsed" data-toggle="collapse" data-parent="#accordion" href="#collapseThree">Secciones en "Crear Ficha"</a>
                </h4>
            </div>
            <div id="collapseThree" class="panel-collapse collapse">
                <div class="panel-body">
                    <strong>Crear ficha </strong>cuenta con varias secciones y botones cuyas 
                    funciones son las siguientes:
                    <ul>
                        <li><strong>Barra de navegación superior:</strong> Desde ella podremos acceder a la 
                            página principal de la aplicacción &quot;Thermolympic S.L.&quot;, acceder la consulta y 
                            modificación de fichas <strong><em>&quot;Consultar Ficha de Fabricación</em></strong>&quot;, 
                            ver el listado de fichas disponibles <strong><em>&quot;Ver listado de fichas&quot;</em></strong> 
                            y acceder al <strong><em>F.A.Q. </em></strong>de la aplicación.</li>
                    </ul>
                    <p>
                        <img class="img-responsive" src="http://facts4-srv/oftecnica/imagenes2/directorio/FAQBarraNavegacion.png" alt=""></p>
                    <ul>
                        <li><strong>Botones de función: </strong>Dos botones están habilitados durante la 
                            creación de una ficha de parámetros:<ul>
                                <li><em><strong>Importar datos de BMS: </strong></em>Cuya función es extraer los 
                                    datos de la referencia a crear de la base de datos de BMS.</li>
                                <li><strong><em>Guardar ficha:</em></strong> Cuya función es guardar todos los 
                                    cambios una que la creación de la ficha ha concluido.</li>
                            </ul>
                        </li>
                    </ul>
                    <p>
                        <img class="img-responsive" src="http://facts4-srv/oftecnica/imagenes2/directorio/FAQNuevaFichaBotones.png" alt=""></p>
                    <ul>
                        <li><strong>Información principal:</strong> En ella se muestran los datos del 
                            producto, así como de los periféricos necesarios para realizar la producción. 
                            Esta documentación junto al bloque de pie de página <strong><em>Datos de la 
                            ficha</em></strong> 
                            es común a todas las secciones de la ficha.</li>
                    </ul>
                    <p>
                        <img class="img-responsive" src="http://facts4-srv/oftecnica/imagenes2/directorio/FAQInformaci%C3%B3nPrincipal.png" alt=""></p>
                    <ul>
                        <li><strong>Parámetros:</strong> La sección parámetros de la página muestra los 
                            distintos datos de configuración de la máquina (temperaturas de inyección, 
                            velocidades y pasos de inyección, postpresión...). Dentro de esta pestaña, 
                            encontraremos además la subsección <strong><em>&quot;Inyección&quot;</em></strong> y
                            <strong><em>&quot;Secuencial&quot;</em></strong>, donde podremos asignar los valores de 
                            inyección dependiendo de la modalidad de inyección de molde.</li>
                    </ul>
                    <p>
                        <img class="img-responsive" src="http://facts4-srv/oftecnica/imagenes2/directorio/FAQParametrosNuevo.png" alt=""></p>
                    </p>
                    <ul>
                        <li><strong>Atemperado:</strong> La sección atemperado de la página muestra la 
                            configuración de conexiones y parámetros de enfriamiento del molde, así como 
                            imágenes de ayuda para el correcto ensamblaje del conjunto de atemperado. Dentro 
                            de esta pesataña, encontraremos dos subsecciones <strong><em>&quot;Parte Fija&quot;</em></strong> 
                            y <strong><em>&quot;Parte Móvil&quot;</em></strong>. </li>
                    </ul>
                    <p>
                        <img class="img-responsive" src="http://facts4-srv/oftecnica/imagenes2/directorio/FAQAtemperadoNuevo.png" alt=""></p>
                </div>
            </div>
        </div>
        <div class="panel panel-default">
            <div class="panel-heading">
                <h4 class="panel-title">
                    <a class="accordion-toggle collapsed" data-toggle="collapse" data-parent="#accordion" href="#collapseFive">Secciones en "Ficha de parámetros"</a>
                </h4>
            </div>
            <div id="collapseFive" class="panel-collapse collapse in">
                <div class="panel-body">
                    <strong>Ficha de parámetros </strong>cuenta con varias secciones y botones cuyas 
                    funciones son las siguientes: 
                    <ul>
                        <li><strong>Barra de navegación superior:</strong> Desde ella podremos acceder a la 
                            página principal de la aplicacción &quot;Thermolympic S.L.&quot;, crear nuevas fichas de 
                            fabricación <strong><em>&quot;Crear ficha</em></strong>&quot;, ver el listado de fichas 
                            disponibles <strong><em>&quot;Ver listado de fichas&quot;;</em></strong> 
                            y acceder al <strong><em>F.A.Q </em></strong>de la aplicación.</li>
                    </ul>
                    <p>
                        Imagen de barra</p>
                    <ul>
                        <li><strong>Área de selección de ficha: </strong>En esta zona seleccionaremos el 
                            número de ficha, la máquina y la versión que queremos cargar para consulta. Este 
                            bloque cuenta con tres selectores y un botón de carga que nos servirá para 
                            buscar y cargar los datos faltantes una vez introducida la referencia.</li>
                    </ul>
                    <p>
                        Imagen de área de selección</p>
                    <ul>
                        <li><strong>Botones de función:  </strong>Dos botones están habilitados durante la 
                            creación de una ficha de parámetros:<ul>
                                <li><em><strong>Modificar ficha: </strong></em>Cuya función es desbloquear el modo 
                                    edición para poder editar la los parámetros guardados.</li>
                                <li><strong><em>Descargar ficha:</em></strong> Cuya función es descargar una copia 
                                    en excel de la ficha de parámetros guardada.</li>
                            </ul>
                        </li>
                    </ul>
                    <p>
                        Imagen de botones de funciónn</p>
                    <ul>
                        <li><strong>Información principal:</strong> En ella se muestran los datos del 
                            producto, así como de los periféricos necesarios para realizar la producción. 
                            Esta documentación junto al bloque de pie de página <strong><em>Datos de la 
                            ficha</em></strong> es común a todas las secciones de la ficha.</li>
                    </ul>
                    <p>
                        Imagen de información principal</p>
                    <ul>
                        <li><strong>Parámetros:</strong> La sección parámetros de la página muestra los 
                            distintos datos de configuración de la máquina (temperaturas de inyección, 
                            velocidades y pasos de inyección, postpresión...). Dentro de esta pestaña, 
                            encontraremos además la subsección <strong><em>&quot;Inyección&quot;</em></strong> y
                            <strong><em>&quot;Secuencial&quot;</em></strong>, donde podremos asignar los valores de 
                            inyección dependiendo de la modalidad de inyección de molde.</li>
                    </ul>
                    <p>
                        Imagen de parámetros</p>
                    <ul>
                        <li><strong>Atemperado:</strong> La sección atemperado de la página muestra la 
                            configuración de conexiones y parámetros de enfriamiento del molde, así como 
                            imágenes de ayuda para el correcto ensamblaje del conjunto de atemperado. Dentro 
                            de esta pesataña, encontraremos dos subsecciones <strong><em>&quot;Parte Fija&quot;</em></strong> 
                            y <strong><em>&quot;Parte Móvil&quot;</em></strong>. </li>
                    </ul>
                    <p>
                        Imagen de atemperado</p>
                    <br />
                </div>
            </div>
        </div>
        <div class="panel panel-default">
            <div class="panel-heading">
                <h4 class="panel-title">
                    <a class="accordion-toggle collapsed" data-toggle="collapse" data-parent="#accordion" href="#collapseSix">Secciones de "Ver listado de fichas"</a>
                </h4>
            </div>
            <div id="collapseSix" class="panel-collapse collapse in">
                <div class="panel-body">
                    There are a number of reasons why you should join us:
                    <ul>
                        <li>A great 70% flat rate for your items.</li>
                        <li>Fast response/approval times. Many sites take weeks to process a theme or template. And if it gets rejected, there is another iteration. We have aliminated this, and made the process very fast. It only takes up to 72 hours for a template/theme to get reviewed.</li>
                        <li>We are not an exclusive marketplace. This means that you can sell your items on <strong>PrepBootstrap</strong>, as well as on any other marketplate, and thus increase your earning potential.</li>
                    </ul>
                </div>
            </div>
        </div>
        <div class="panel panel-default">
            <div class="panel-heading">
                <h4 class="panel-title">
                    <a class="accordion-toggle collapsed" data-toggle="collapse" data-parent="#accordion" href="#collapseEight">What are the payment options?</a>
                </h4>
            </div>
            <div id="collapseEight" class="panel-collapse collapse">
                <div class="panel-body">
                    The best way to transfer funds is via Paypal. This secure platform ensures timely payments and a secure environment. 
                </div>
            </div>
        </div>
        <div class="panel panel-default">
            <div class="panel-heading">
                <h4 class="panel-title">
                    <a class="accordion-toggle collapsed" data-toggle="collapse" data-parent="#accordion" href="#collapseNine">When do I get paid?</a>
                </h4>
            </div>
            <div id="collapseNine" class="panel-collapse collapse">
                <div class="panel-body">
                    Our standard payment plan provides for monthly payments. At the end of each month, all accumulated funds are transfered to your account. 
                    The minimum amount of your balance should be at least 70 USD. 
                </div>
            </div>
        </div>

        <div class="faqHeader">Crear, consultar y modificar una ficha</div>
        <div class="panel panel-default">
            <div class="panel-heading">
                <h4 class="panel-title">
                    <a class="accordion-toggle collapsed" data-toggle="collapse" data-parent="#accordion" href="#collapseFour">I want to buy a theme - what are the steps?</a>
                </h4>
            </div>
            <div id="collapseFour" class="panel-collapse collapse">
                <div class="panel-body">
                    Buying a theme on <strong>PrepBootstrap</strong> is really simple. Each theme has a live preview. 
                    Once you have selected a theme or template, which is to your liking, you can quickly and securely pay via Paypal.
                    <br />
                    Once the transaction is complete, you gain full access to the purchased product. 
                </div>
            </div>
        </div>
        <div class="panel panel-default">
            <div class="panel-heading">
                <h4 class="panel-title">
                    <a class="accordion-toggle collapsed" data-toggle="collapse" data-parent="#accordion" href="#collapseSeven">Is this the latest version of an item</a>
                </h4>
            </div>
            <div id="collapseSeven" class="panel-collapse collapse">
                <div class="panel-body">
                    Each item in <strong>PrepBootstrap</strong> is maintained to its latest version. This ensures its smooth operation.
                </div>
            </div>
        </div>
    </div>
</div>

<style>
    .faqHeader {
        font-size: 27px;
        margin: 20px;
    }

    .panel-heading [data-toggle="collapse"]:after {
        font-family: 'Glyphicons Halflings';
        content: "\e072"; /* "play" icon */
        float: right;
        color: #F58723;
        font-size: 18px;
        line-height: 22px;
        /* rotate "play" icon from > (right arrow) to down arrow */
        -webkit-transform: rotate(-90deg);
        -moz-transform: rotate(-90deg);
        -ms-transform: rotate(-90deg);
        -o-transform: rotate(-90deg);
        transform: rotate(-90deg);
    }

    .panel-heading [data-toggle="collapse"].collapsed:after {
        /* rotate "play" icon from > (right arrow) to ^ (up arrow) */
        -webkit-transform: rotate(90deg);
        -moz-transform: rotate(90deg);
        -ms-transform: rotate(90deg);
        -o-transform: rotate(90deg);
        transform: rotate(90deg);
        color: #454444;
    }
</style>

<!-- Bootstrap FAQ - END -->
    



    </form>
</body>
</html>
