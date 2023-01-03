let modal = new bootstrap.Modal(document.getElementById('myModal'), {
    keyboard: false
})

function SetModal() {
    

    $(document).ready(function () {
        $(function () {
            $.ajaxSetup({ cache: false });

            $("a[data-modal]").on("click",
                function (e) {
                    $('#myModalContent').load(this.href,
                        function () {
                            //$('#myModal').modal({keyboard: true}, 'show');
                            modal.show()
                            bindForm(this);
                        });
                    return false;
                });
        });
    });
}

function bindForm(dialog) {
    $('form', dialog).submit(function () {
        $.ajax({
            url: this.action,
            type: this.method,
            data: $(this).serialize(),
            success: function (result) {
                if (result.success) {
                    //$('#myModal').modal('hide');
                    modal.hide()
                    $('#AddressTarget').load(result.url); // Carrega o resultado HTML para a div demarcada
                } else {
                    $('#myModalContent').html(result);
                    bindForm(dialog);
                }
            }
        });

        SetModal();
        return false;
    });
}

function SearchForZipCode() {
    $(document).ready(function () {
        const ADDRESS_STREET_ID = '#Address_Street'
        const ADDRESS_NEIGHBORHOOD_ID = '#Address_Neighborhood'
        const ADDRESS_CITY_ID = '#Address_City'
        const ADDRESS_STATE_ID = '#Address_State'
        const ADDRESS_ZIPCODE_ID = '#Address_ZipCode'

        function clearZipCodeForm() {
            // Limpa valores do formulário de cep.
            $(ADDRESS_STREET_ID).val("");
            $(ADDRESS_NEIGHBORHOOD_ID).val("");
            $(ADDRESS_CITY_ID).val("");
            $(ADDRESS_STATE_ID).val("");
        }

        //Quando o campo cep perde o foco.
        $(ADDRESS_ZIPCODE_ID).blur(function () {

            //Nova variável "cep" somente com dígitos.
            var cep = $(this).val().replace(/\D/g, '');

            //Verifica se campo cep possui valor informado.
            if (cep != "") {

                //Expressão regular para validar o CEP.
                var validacep = /^[0-9]{8}$/;

                //Valida o formato do CEP.
                if (validacep.test(cep)) {

                    //Preenche os campos com "..." enquanto consulta webservice.
                    $(ADDRESS_STREET_ID).val("...");
                    $(ADDRESS_NEIGHBORHOOD_ID).val("...");
                    $(ADDRESS_CITY_ID).val("...");
                    $(ADDRESS_STATE_ID).val("...");

                    //Consulta o webservice viacep.com.br/
                    $.getJSON("https://viacep.com.br/ws/" + cep + "/json/?callback=?",
                        function (dados) {

                            if (!("erro" in dados)) {
                                //Atualiza os campos com os valores da consulta.
                                $(ADDRESS_STREET_ID).val(dados.logradouro);
                                $(ADDRESS_NEIGHBORHOOD_ID).val(dados.bairro);
                                $(ADDRESS_CITY_ID).val(dados.localidade);
                                $(ADDRESS_STATE_ID).val(dados.uf);
                            } //end if.
                            else {
                                //CEP pesquisado não foi encontrado.
                                clearZipCodeForm();
                                alert("ZipCode not found.");
                            }
                        });
                } //end if.
                else {
                    //cep é inválido.
                    clearZipCodeForm();
                    alert("ZipCode format is invalid.");
                }
            } //end if.
            else {
                //cep sem valor, limpa formulário.
                clearZipCodeForm();
            }
        });
    });
}

$(document).ready(function () {
    $("#msg_box").fadeOut(2500);
});