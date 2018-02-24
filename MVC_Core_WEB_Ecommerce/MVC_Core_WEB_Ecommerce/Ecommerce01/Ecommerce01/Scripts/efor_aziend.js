  $(document).ready(function () {
            $("#CityId").change(function () {
                $("#CompanyId").empty();
                $("#CompanyId").append('<option value="0">[Selezione una Azienda...]</option>');
                    $.ajax({
                        type: 'POST',
                        url: Url3,
                        dataType: 'json',
                        data: { cityId: $("#CityId").val() },
                        success: function (data) {
                            $.each(data, function (i, data) {
                                $("#CompanyId").append('<option value="'
                                    + data.CompanyId + '">'
                                    + data.Name + '</option>');
                            });
                        },
                        error: function (ex) {
                            alert('Errore nel prendere le Aziende' + ex);
                        }
                    });
                    return false;
      })

  });
