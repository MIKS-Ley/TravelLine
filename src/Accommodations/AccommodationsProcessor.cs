using Accommodations.Commands;
using Accommodations.Dto;

namespace Accommodations;

public static class AccommodationsProcessor
{
    private static readonly BookingService _bookingService = new();
    private static readonly Dictionary<int, ICommand> _executedCommands = new();
    private static int s_commandIndex = 0;

    public static void Run()
    {
        Console.WriteLine( "Booking Command Line Interface" );
        Console.WriteLine( "Commands:" );
        Console.WriteLine( "'book <UserId> <Category> <StartDate> <EndDate> <Currency>' - to book a room" );
        Console.WriteLine( "'cancel <BookingId>' - to cancel a booking" );
        Console.WriteLine( "'undo' - to undo the last command" );
        Console.WriteLine( "'find <BookingId>' - to find a booking by ID" );
        Console.WriteLine( "'search <StartDate> <EndDate> <CategoryName>' - to search bookings" );
        Console.WriteLine( "'exit' - to exit the application" );

        string input;
        while ( ( input = Console.ReadLine() ) != "exit" )
        {
            try
            {
                ProcessCommand( input );
            }
            catch ( Exception ex ) when ( ex is ArgumentException or InvalidOperationException or InvalidDataException )
            {
                Console.WriteLine( $"Error: {ex.Message}" );
            }
        }
    }

    private static void ProcessCommand( string input )
    {
        string[] parts = input.Split( ' ' );
        string commandName = parts[ 0 ];

        switch ( commandName )
        {
            case "book":
                ProcessBookCommand( parts );
                break;

            case "cancel":
                ProcessCancelCommand( parts );
                break;

            case "undo":
                ProcessUndoCommand();
                break;

            case "find":
                ProcessFindCommand( parts );
                break;

            case "search":
                ProcessSearchCommand( parts );
                break;

            default:
                Console.WriteLine( "Unknown command." );
                break;
        }
    }

    private static void ProcessBookCommand( string[] parts )
    {
        if ( parts.Length != 6 )
        {
            throw new ArgumentException( "Invalid number of arguments for booking. Expected format: 'book <UserId> <Category> <StartDate> <EndDate> <Currency>'" );
        }

        if ( !int.TryParse( parts[ 1 ], out int userId ) )
        {
            throw new ArgumentException( "Invalid User ID. Must be an integer." );
        }

        DateTime startDate = ParseDate( parts[ 3 ], "Invalid start date format. Use format like '3/30/2024'." );
        DateTime endDate = ParseDate( parts[ 4 ], "Invalid end date format. Use format like '4/1/2024'." );

        if ( startDate >= endDate )
        {
            throw new ArgumentException( "Start date must be before end date." );
        }

        if ( !Enum.TryParse( parts[ 5 ], true, out CurrencyDto currency ) )
        {
            string availableCurrencies = string.Join( ", ", Enum.GetNames( typeof( CurrencyDto ) ) );
            throw new ArgumentException( $"Invalid currency: '{parts[ 5 ]}'. Available currencies: {availableCurrencies.ToLower()}" );
        }

        BookingDto bookingDto = new()
        {
            UserId = userId,
            Category = parts[ 2 ],
            StartDate = startDate,
            EndDate = endDate,
            Currency = currency,
        };

        BookCommand bookCommand = new( _bookingService, bookingDto );
        bookCommand.Execute();
        _executedCommands.Add( ++s_commandIndex, bookCommand );
        Console.WriteLine( "Booking created successfully." );
    }

    private static void ProcessCancelCommand( string[] parts )
    {
        if ( parts.Length != 2 )
        {
            throw new ArgumentException( "Invalid number of arguments for canceling. Expected format: 'cancel <BookingId>'" );
        }

        if ( !Guid.TryParse( parts[ 1 ], out Guid bookingId ) )
        {
            throw new ArgumentException( "Invalid booking ID format. Must be a valid GUID." );
        }

        CancelBookingCommand cancelCommand = new( _bookingService, bookingId );
        cancelCommand.Execute();
        _executedCommands.Add( ++s_commandIndex, cancelCommand );
        Console.WriteLine( "Booking cancelled successfully." );
    }

    private static void ProcessUndoCommand()
    {
        if ( _executedCommands.Count == 0 )
        {
            throw new InvalidOperationException( "No commands to undo." );
        }

        _executedCommands[ s_commandIndex ].Undo();
        _executedCommands.Remove( s_commandIndex );
        s_commandIndex--;
        Console.WriteLine( "Last command undone successfully." );
    }

    private static void ProcessFindCommand( string[] parts )
    {
        if ( parts.Length != 2 )
        {
            throw new ArgumentException( "Invalid arguments for 'find'. Expected format: 'find <BookingId>'" );
        }

        if ( !Guid.TryParse( parts[ 1 ], out Guid id ) )
        {
            throw new ArgumentException( "Invalid booking ID format. Must be a valid GUID." );
        }

        FindBookingByIdCommand findCommand = new( _bookingService, id );
        findCommand.Execute();
    }

    private static void ProcessSearchCommand( string[] parts )
    {
        if ( parts.Length != 4 )
        {
            throw new ArgumentException( "Invalid arguments for 'search'. Expected format: 'search <StartDate> <EndDate> <CategoryName>'" );
        }

        DateTime startDate = ParseDate( parts[ 1 ], "Invalid start date format." );
        DateTime endDate = ParseDate( parts[ 2 ], "Invalid end date format." );

        if ( startDate >= endDate )
        {
            throw new ArgumentException( "Start date must be before end date." );
        }

        string categoryName = parts[ 3 ];
        SearchBookingsCommand searchCommand = new( _bookingService, startDate, endDate, categoryName );
        searchCommand.Execute();
    }

    private static DateTime ParseDate( string dateString, string errorMessage )
    {
        if ( !DateTime.TryParse( dateString, out DateTime result ) )
        {
            throw new InvalidDataException( errorMessage );
        }
        return result;
    }
}