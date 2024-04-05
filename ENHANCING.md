Melhorias:

-- DATABASE --
Alterar no banco de dados:

TABLE: dbo.Attendee

Alterar tabela de "CheckIn" e retirar o campo "Attendee_Id" e criar o campo "CheckIn_Id" na tabela de "Attendees"

depois retirar o Attendee { get; set; } do Checkin da classe de infraestrutura e fazer esse relacionamento na classe Attendee no campo do tipo Checkin "Checkin".

