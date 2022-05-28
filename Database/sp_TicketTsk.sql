USE [TicketDB]
GO

/****** Object:  StoredProcedure [dbo].[sp_TicketTsk]    Script Date: 5/28/2022 7:20:28 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- EXEC	[dbo].[sp_TicketTsk] @action = 'add', @Id = null, @FullName='Gaurav', @TicketType = 'adult', @Price=123.11
CREATE Proc [dbo].[sp_TicketTsk](
	@action nvarchar(10) = null,
	@Id Int = null,
	@FullName NVARCHAR (100)= null,
	@TicketType nvarchar(50) = null,
	@Price decimal(10,2) = null
)
As 
Begin

if @action = 'add'
	Begin
		Insert into Ticket(FullName, TicketType, Price,EntryDate, CreateDate) values(
		@FullName
		,@TicketType
		,@Price,
		null,
		GETDATE()
		)
		select 'Added Successfully' as msg
	End

else if @action = 'scan'
	Begin

	Declare @isTicketValid bit;
	set @isTicketValid = isnull((select 1 from Ticket as t where t.Id = @Id and CAST(t.CreateDate as date) = cast(GETDATE() as date)),0)
	if(@isTicketValid = 1)
		begin
	Declare @EntryDate datetime;
	select @EntryDate = EntryDate from ticket as t where t.id = @id
	select @EntryDate
	if(isnull(@EntryDate,'') = '' )
	begin
		update t
		set t.EntryDate = GETDATE()
		from Ticket as t where t.Id = @Id
		select 'Welcome to fun park.'
	end
	else
	begin
	select 'Ticket has been already used' as response
	end
	end
	else
	begin
	select 'Ticket is invalid or expired' as response
	end
	End
	else if @action = 'select'
	begin
		select * from Ticket as t with(nolock)
	end
	else if @action = 'get'
	begin
		select *from Ticket where id = @Id
	end
	else
	begin
		select 'invalid action found!' as response
	end
End

GO


