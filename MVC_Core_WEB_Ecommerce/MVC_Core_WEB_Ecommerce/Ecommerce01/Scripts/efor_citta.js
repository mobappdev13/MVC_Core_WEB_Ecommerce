  $(document).ready(function () {
            $("#ProvinceId").change(function () {
                $("#CityId").empty();
                $("#CityId").append('<option value="0">[Selezione una Città...]</option>');
                    $.ajax({
                        type: 'POST',
                        url: Url2,
                        dataType: 'json',
                        data: { provinceId: $("#ProvinceId").val() },
                        success: function (data) {
                            $.each(data, function (i, data) {
                                $("#CityId").append('<option value="'
                                    + data.CityId + '">'
                                    + data.Name + '</option>');
                            });
                        },
                        error: function (ex) {
                            alert('Errore nel prendere le Città' + ex);
                        }
                    });
                    return false;
      })

  });
