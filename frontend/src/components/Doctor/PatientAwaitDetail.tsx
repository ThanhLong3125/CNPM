import React from "react"
import { useParams } from "react-router-dom"
const PatentAwaitDetail: React.FC = () => {


    const { patient_id } = useParams();
    console.log({ patient_id })


    return (
        <div className="m-7">
            <h2>Hhhhhhhh</h2>
        </div>
    )
}

export default PatentAwaitDetail