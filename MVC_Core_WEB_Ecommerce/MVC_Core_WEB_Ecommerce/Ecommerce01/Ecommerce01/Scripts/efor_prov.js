  $(document).ready(function () {
            $("#DepartamentId").change(function () {
                $("#ProvinceId").empty();
                $("#ProvinceId").append('<option value="0">[Selezione una Provincia...]</option>');
                    $.ajax({
                        type: 'POST',
                        url: Url,
                        dataType: 'json',
                        data: { departamentId: $("#DepartamentId").val() },
                        success: function (data) {
                            $.each(data, function (i, data) {
                                $("#ProvinceId").append('<option value="'
                                    + data.ProvinceId + '">'
                                    + data.Name + '</option>');
                            });
                        },
                        error: function (ex) {
                            alert('Errore nel prendere le Provincie' + ex);
                        }
                    });
                    return false;
      })

  });
