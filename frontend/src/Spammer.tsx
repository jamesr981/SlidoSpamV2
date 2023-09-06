import {useEffect, useState} from "react";
import {RotatingLines} from "react-loader-spinner";
import SlidoSpammerControl from "./SlidoSpammerControl.tsx";

interface SpammerProps {
    slidoGuid: string
}

interface Section {
    eventSectionId: number,
    isActive: boolean,
    isDeleted: boolean,
    name: string
}

interface SlidoEvent {
    name: string,
    eventId: number,
    sections: Section[]
}

function Spammer(props: SpammerProps) {
    const {slidoGuid} = props
    const [slidoEvent, setSlidoEvent] = useState<SlidoEvent | null>(null)
    const [isEventLoading, setIsEventLoading] = useState<boolean>(false)
    const [isSpamming, setIsSpamming] = useState<boolean>(false)

    useEffect(() => {
        const getSlidoEvent = async () => {
            setIsEventLoading(true)
            const response = await fetch(`api/Events/${slidoGuid}`)
            const event = await response.json()
            
            if (!event) return;

            setSlidoEvent({
                name: event.name,
                eventId: event.event_id,
                sections: event.sections.map((section: any) => {
                    return {
                        eventSectionId: section.event_section_id,
                        isActive: section.is_active,
                        isDeleted: section.is_deleted,
                        name: section.name
                    }
                })
            })
            setIsEventLoading(false)
        }

        getSlidoEvent().catch(console.error);
    }, [])

    const onQuestionSpamBtnClick = async (inputValue: string) => {
        setIsSpamming(true)

        if (!slidoEvent) throw Error('No Event')
        const sectionId = slidoEvent.sections?.find((section) => section.isActive && !section.isDeleted)?.eventSectionId
        if (!sectionId) throw Error('Unable to get section id')

        const payload = {
            eventId: slidoEvent.eventId,
            questionCount: inputValue as unknown as number,
            postAnonymously: false,
            eventSectionId: sectionId
        }

        await fetch(`api/Events/${slidoGuid}/SpamQuestions`, {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(payload)
        })

        setIsSpamming(false)
    }

    if (isEventLoading) return (
        <div>
            <RotatingLines/>
        </div>
    )

    if (!slidoEvent) return (
        <div>
            <h2>Failed to connect to a Slido event with the Id: {slidoGuid}</h2>
        </div>
    )

    return (
        <div>
            <h2>You are connected to {slidoEvent.name}</h2>
            {isSpamming ? <h2>Spamming...</h2> : null}
            <SlidoSpammerControl inputType={"number"} onControlSubmit={onQuestionSpamBtnClick} buttonLabel="Create"
                                 label="Number of questions to create: " disabled={isSpamming}/>
        </div>
    )
}

export default Spammer